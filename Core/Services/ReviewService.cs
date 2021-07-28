using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Identity;
using Storage.Identity;
using Storage.Interfaces;
using Storage.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IGetterService<Book> _bookGetter;
        private readonly ILoggedUserProvider _loggedUserProvider;
        private readonly UserManager<User> _userManager;

        public ReviewService(IReviewRepository reviewRepository, IGetterService<Book> bookGetter, 
            ILoggedUserProvider loggedUserProvider, UserManager<User> userManager)
        {
            _loggedUserProvider = loggedUserProvider;
            _reviewRepository = reviewRepository;
            _bookGetter = bookGetter;
            _userManager = userManager;
        }

        public async Task<ServiceResponse> AddReview(ReviewRequest review)
        {
            if ((await _bookGetter.GetById<BookDto>(review.BookId)).Content == null)
                return ServiceResponse.Error($"The book you are trying to post a review for doesn't exist", HttpStatusCode.BadRequest);

            var userId = _loggedUserProvider.GetUserId();
            if (await _reviewRepository.ReviewByUserExists(userId, review.BookId)) return ServiceResponse.Error
                    ($"You have already posted a review for {await GetBookTitle(review.BookId)}", HttpStatusCode.BadRequest);

            var newReview = await _reviewRepository.CreateReview(review);
            return ServiceResponse<ReviewDto>.Success(newReview, "Review created.", HttpStatusCode.Created);
        }

        public async Task<ServiceResponse> GetReviews(int? bookId)
        {
            if (bookId == null) return ServiceResponse<IEnumerable<ReviewDto>>.Success
                    (await _reviewRepository.GetReviews(bookId), $"All reviews retrieved.");

            if (await _bookGetter.GetById<BookDto>((int)bookId) == null)
                return ServiceResponse.Error($"The book you are trying to find a review for doesn't exist", HttpStatusCode.NotFound);

            return ServiceResponse<IEnumerable<ReviewDto>>.Success
                    (await _reviewRepository.GetReviews(bookId), $"Reviews for {await GetBookTitle((int)bookId)} retrieved.");
        }

        public async Task<ServiceResponse> GetReview(int reviewId)
        {
            var review = await _reviewRepository.GetReview(reviewId);
            if (review != null) return ServiceResponse<ReviewDto>.Success (review, $"Review retrieved.");
            return ServiceResponse.Error("Review not found.", HttpStatusCode.NotFound);
        }

        private async Task<string> GetBookTitle(int bookId)
        {
            return (await _bookGetter.GetById<BookDto>(bookId)).Content.Title;
        }
    }
}

