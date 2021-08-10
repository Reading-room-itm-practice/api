using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Storage.Interfaces;
using Storage.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Storage.Models.Likes;

namespace Core.Services
{
    internal class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IGetterService<Book> _bookGetter;
        private readonly ILoggedUserProvider _loggedUserProvider;
        private readonly IMapper _mapper;
        private ILikeableMapperHelper<ReviewLike, Review, ReviewDto> _mapperHelper;

        public ReviewService(IReviewRepository reviewRepository, IGetterService<Book> bookGetter, 
            ILoggedUserProvider loggedUserProvider, IMapper mapper, ILikeableMapperHelper<ReviewLike, Review, ReviewDto> mapperHelper)
        {
            _loggedUserProvider = loggedUserProvider;
            _reviewRepository = reviewRepository;
            _bookGetter = bookGetter;
            _mapper = mapper;
            _mapperHelper = mapperHelper;
        }

        public async Task<ServiceResponse> AddReview(ReviewRequest review)
        {
            if ((await _bookGetter.GetById<BookDto>(review.BookId)).Content == null)
            {
                return ServiceResponse.Error($"The book you are trying to post a review for doesn't exist", HttpStatusCode.BadRequest);
            }
            var userId = _loggedUserProvider.GetUserId();
            
            if (await _reviewRepository.ReviewByUserExists(userId, review.BookId))
            {
                return ServiceResponse.Error($"You have already posted a review for {await GetBookTitle(review.BookId)}", HttpStatusCode.BadRequest);
            }
            var newReview = await _reviewRepository.CreateReview(_mapper.Map<Review>(review));
            
            return ServiceResponse<ReviewDto>.Success(_mapperHelper.Map(newReview), "Review created.", HttpStatusCode.Created);
        }

        public async Task<ServiceResponse> GetReviews(int? bookId)
        {
            if (bookId == null)
            {
                var reviews = await _reviewRepository.GetReviews();

                return ServiceResponse<IEnumerable<ReviewDto>>.Success(_mapper.Map<IEnumerable<ReviewDto>>(reviews), "All reviews retrieved.");
            }

            if ((await _bookGetter.GetById<BookDto>((int) bookId)).Content == null)
            {
                return ServiceResponse.Error("The book you are trying to find a review for doesn't exist", HttpStatusCode.NotFound);
            }

            var bookReviews = await _reviewRepository.GetReviews(bookId);
            var bookTitle = await GetBookTitle((int) bookId);

            return ServiceResponse<IEnumerable<ReviewDto>>.Success(_mapperHelper.Map(bookReviews), $"Reviews for {bookTitle} retrieved.");
        }

        public async Task<ServiceResponse> GetReview(int reviewId)
        {
            var review = await _reviewRepository.GetReview(reviewId);
            
            if (review != null)
            {
                return ServiceResponse<ReviewDto>.Success (_mapperHelper.Map(review), $"Review retrieved.");
            }

            return ServiceResponse.Error("Review not found.", HttpStatusCode.NotFound);
        }

        private async Task<string> GetBookTitle(int bookId)
        {
            return (await _bookGetter.GetById<BookDto>(bookId)).Content.Title;
        }
    }
}

