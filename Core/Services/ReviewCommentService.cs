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
        public ReviewCommentService(IReviewCommentRepository reviewCommentRepository, ILoggedUserProvider loggedUserProvider,
            IGetterService<Review> reviewGetter)
        {
            _reviewCommentRepository = reviewCommentRepository;
            _loggedUserProvider = loggedUserProvider;
            _reviewGetter = reviewGetter;
        }
        public async Task<ServiceResponse> AddReviewComment(ReviewCommentRequest comment)
        {
            if (await _reviewGetter.GetById<Review>(comment.ReviewId) == null) return new ErrorResponse()
            {
                Message = $"Review with Id: {comment.ReviewId} doesn't exist.",
                StatusCode = HttpStatusCode.BadRequest
            };

            var userId = _loggedUserProvider.GetUserId();
            if (await _reviewCommentRepository.CheckCommentCount(comment.ReviewId, userId)) return new ErrorResponse()
            {
                Message = $"A single user can post only {_reviewCommentRepository.MaxCommentPerReview} " +
                    $"comments per single review (Id: {comment.ReviewId})",
                StatusCode = HttpStatusCode.BadRequest
            };

            if (await _reviewCommentRepository.CheckCommentsDate(comment.ReviewId, userId)) return new ErrorResponse()
            {
                Message = $"A single user can post only {_reviewCommentRepository.MaxCommentPerHourPerReview} " +
                    $"comments per hour on a single review (Id: {comment.ReviewId}). Try again later",
                StatusCode = HttpStatusCode.BadRequest
            };

            return new SuccessResponse<ReviewCommentDto>()
            {
                Message = "Comment posted.",
                StatusCode = HttpStatusCode.Created,
                Content = await _reviewCommentRepository.CreateReviewComment(comment)
            };
        }

        public async Task<ServiceResponse> GetComments(int? reviewId, int? userId, bool? currentUser)
        {
            if (reviewId != null && await _reviewGetter.GetById<Review>((int)reviewId) == null) return new ErrorResponse()
            {
                Message = $"Review with Id: {reviewId} doesn't exist.",
                StatusCode = HttpStatusCode.BadRequest
            };

            if (reviewId == null && userId == null && currentUser != true) return new SuccessResponse<IEnumerable<ReviewCommentDto>>()
            {
                Message = $"All comments retrieved.",
                Content = await _reviewCommentRepository.GetComments()
            };

            if (reviewId != null && userId == null && currentUser != true) return new SuccessResponse<IEnumerable<ReviewCommentDto>>()
            {
                Message = $"All comments for review with Id: {reviewId} retrieved.",
                Content = await _reviewCommentRepository.GetComments(reviewId, userId)
            };
            
            if (reviewId == null && userId != null && currentUser != true) return new SuccessResponse<IEnumerable<ReviewCommentDto>>()
            {
                Message = $"All comments by user with retrieved.",
                Content = await _reviewCommentRepository.GetComments(reviewId, userId)
            };

            if (reviewId != null && userId != null && currentUser != true) return new SuccessResponse<IEnumerable<ReviewCommentDto>>()
            {
                Message = $"All comments for review with Id: {reviewId} by user retrieved.",
                Content = await _reviewCommentRepository.GetComments(reviewId, userId)
            };

            if (reviewId == null && currentUser == true)
            {
                userId = _loggedUserProvider.GetUserId();
                return new SuccessResponse<IEnumerable<ReviewCommentDto>>()
                {
                    Message = $"All comments by current user retrieved.",
                    Content = await _reviewCommentRepository.GetComments(reviewId, userId)
                };
            }

            if (reviewId != null && currentUser == true)
            {
                userId = _loggedUserProvider.GetUserId();
                return new SuccessResponse<IEnumerable<ReviewCommentDto>>()
                {
                    Message = $"All comments for review with Id: {reviewId} by current user retrieved.",
                    Content = await _reviewCommentRepository.GetComments(reviewId, userId)
                };
            }
            return new ErrorResponse() { StatusCode = HttpStatusCode.BadRequest };
        }
    }
}
