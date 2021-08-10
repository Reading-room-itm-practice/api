using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Storage.Interfaces;
using Storage.Models;
using Storage.Models.Likes;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Core.Services
{
    public class ReviewCommentService : IReviewCommentService
    {
        private readonly IReviewCommentRepository _reviewCommentRepository;
        private readonly ILoggedUserProvider _loggedUserProvider;
        private readonly IReviewRepository _reviewRepository;
        private readonly IGetterService<Book> _bookGetter;
        private readonly IMapper _mapper;
        private readonly ILikeableMapperHelper<ReviewCommentLike, ReviewComment, ReviewCommentDto> _mapperHelper;

        public ReviewCommentService(IReviewCommentRepository reviewCommentRepository,
            ILoggedUserProvider loggedUserProvider,
            IReviewRepository reviewRepository, IGetterService<Book> bookGetter,
            ILikeableMapperHelper<ReviewCommentLike, ReviewComment, ReviewCommentDto> mapperHelper,
            IMapper mapper)
        {
            _reviewCommentRepository = reviewCommentRepository;
            _loggedUserProvider = loggedUserProvider;
            _reviewRepository = reviewRepository;
            _bookGetter = bookGetter;
            _mapperHelper = mapperHelper;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> AddReviewComment(ReviewCommentRequest request)
        {
            if (!(await _reviewCommentRepository.ReviewExists(request.ReviewId)))
            {
                return ServiceResponse.Error($"Review you're trying to post a request for doesn't exist.", HttpStatusCode.BadRequest);
            }

            var userId = _loggedUserProvider.GetUserId();
            if (await _reviewCommentRepository.HitCommentCapCount(request.ReviewId, userId))
            {
                return ServiceResponse.Error(
                    $"You can post only {_reviewCommentRepository.MaxCommentPerReview} comments per single review", HttpStatusCode.BadRequest);
            }
       

            if (await _reviewCommentRepository.HitCommentCapDate(request.ReviewId, userId))
            {
                return ServiceResponse.Error(
                    $"You can post only {_reviewCommentRepository.MaxCommentPerHourPerReview} comments per hour on a single review. Try again later", HttpStatusCode.BadRequest);

            }
            var reviewComment = await _reviewCommentRepository.Create(_mapper.Map<ReviewComment>(request));

            return ServiceResponse<ReviewCommentDto>.Success(_mapperHelper.Map(reviewComment), "Comment posted.", HttpStatusCode.Created);

        }

        public async Task<ServiceResponse> GetComment(int reviewCommentId)
        {
            var reviewComment = await _reviewCommentRepository.GetComment(reviewCommentId);

            return reviewComment != null ? ServiceResponse<ReviewCommentDto>.Success(_mapperHelper.Map(reviewComment), "Comment retrieved.") 
                : ServiceResponse.Error("Comment not found.", HttpStatusCode.NotFound);
        }

        public async Task<ServiceResponse> GetComments(int? reviewId, Guid? userId, bool currentUser)
        {
            if (reviewId != null && !(await _reviewCommentRepository.ReviewExists((int) reviewId)))
            {
                return ServiceResponse.Error("Review doesn't exist.", HttpStatusCode.BadRequest);
            }

            if (currentUser)
            {
                userId = _loggedUserProvider.GetUserId();
            }

            var message = await CreateMessage(reviewId, userId, currentUser);
            var comments = _reviewCommentRepository.GetComments(reviewId, userId);

            return ServiceResponse<IEnumerable<ReviewCommentDto>>.Success(_mapperHelper.Map(comments), message);
        }

        private async Task<string> CreateMessage(int? reviewId, Guid? userId, bool currentUser)
        {
            var message = new StringBuilder("All comments ");
            
            if (reviewId.HasValue)
            {
                message.Append($"for review of {await GetBookTitle((int) reviewId)} ");
            }
            if (currentUser)
            {
                message.Append("by current user ");
            }
            else if (userId != null)
            {
                message.Append("by user ");
            }
            message.Append("retrieved.");
            
            return message.ToString();
        }

        private async Task<string> GetBookTitle(int reviewId)
        {
            return (await _bookGetter.GetById<BookDto>((await _reviewRepository.GetReview(reviewId)).BookId)).Content.Title;
        }
    }
}
