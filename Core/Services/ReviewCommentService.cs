using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Storage.Interfaces;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ReviewCommentService : IReviewCommentService
    {
        private readonly IReviewCommentRepository _reviewCommentRepository;
        private readonly ILoggedUserProvider _loggedUserProvider;
        private readonly IGetterService<Review> _reviewGetter;
        private readonly IGetterService<Book> _bookGetter;
        public ReviewCommentService(IReviewCommentRepository reviewCommentRepository, ILoggedUserProvider loggedUserProvider,
            IGetterService<Review> reviewGetter, IGetterService<Book> bookGetter)
        {
            _reviewCommentRepository = reviewCommentRepository;
            _loggedUserProvider = loggedUserProvider;
            _reviewGetter = reviewGetter;
            _bookGetter = bookGetter;
        }
        public async Task<ServiceResponse> AddReviewComment(ReviewCommentRequest comment)
        {
            if (await _reviewGetter.GetById<Review>(comment.ReviewId) == null) return new ErrorResponse()
            {
                Message = $"Review doesn't exist.",
                StatusCode = HttpStatusCode.BadRequest
            };

            var userId = _loggedUserProvider.GetUserId();
            if (await _reviewCommentRepository.CheckCommentCount(comment.ReviewId, userId)) return new ErrorResponse()
            {
                Message = $"You can post only {_reviewCommentRepository.MaxCommentPerReview} " +
                    $"comments per single review",
                StatusCode = HttpStatusCode.BadRequest
            };

            if (await _reviewCommentRepository.CheckCommentsDate(comment.ReviewId, userId)) return new ErrorResponse()
            {
                Message = $"You can post only {_reviewCommentRepository.MaxCommentPerHourPerReview} " +
                    $"comments per hour on a single review. Try again later",
                StatusCode = HttpStatusCode.BadRequest
            };

            return new SuccessResponse<ReviewCommentDto>()
            {
                Message = "Comment posted.",
                StatusCode = HttpStatusCode.Created,
                Content = await _reviewCommentRepository.CreateReviewComment(comment)
            };
        }

        public async Task<ServiceResponse> GetComments(int? reviewId, int? userId, bool currentUser)
        {
            if (reviewId != null && await _reviewGetter.GetById<Review>((int)reviewId) == null) return new ErrorResponse()
            {
                Message = $"Review doesn't exist.",
                StatusCode = HttpStatusCode.BadRequest
            };

            if (reviewId == null && userId == null && currentUser != true) return new SuccessResponse<IEnumerable<ReviewCommentDto>>()
            {
                Message = $"All comments retrieved.",
                Content = await _reviewCommentRepository.GetComments()
            };

            var message = await CreateMessage(reviewId, userId, currentUser);
            if (currentUser) userId = _loggedUserProvider.GetUserId();
            var comments = await _reviewCommentRepository.GetComments(reviewId, userId);

            return new SuccessResponse<IEnumerable<ReviewCommentDto>>()
            {
                Message = message,
                Content = comments
            };
        }

        private async Task<string> CreateMessage(int? reviewId, int? userId, bool currentUser)
        {
            StringBuilder message = new StringBuilder("All comments ");
            if (reviewId.HasValue) message.Append($"for review of {await GetBookTitle((int)reviewId)} ");
            if (currentUser) message.Append("by current user ");
            else if (userId != null) message.Append("by user ");
            message.Append("retrieved.");
            return message.ToString();
        }

        private async Task<string> GetBookTitle(int reviewId)
        {
            var book = await _bookGetter.GetById<BookDto>((await _reviewGetter.GetById<ReviewDto>(reviewId)).BookId);
            return book.Title;
        }
    }
}
