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

        public ReviewService(IReviewRepository reviewRepository, IGetterService<Book> bookGetter, 
            ILoggedUserProvider loggedUserProvider)
        {
            _loggedUserProvider = loggedUserProvider;
            _reviewRepository = reviewRepository;
            _bookGetter = bookGetter;
        }

        public async Task<ServiceResponse> AddReview(ReviewRequest review)
        {
            if (await _bookGetter.GetById<BookDto>(review.BookId) == null)
            {
                return ServiceResponse.Error($"Book with Id: {review.BookId} doesn't exist", HttpStatusCode.BadRequest);
            }

            var userId = _loggedUserProvider.GetUserId();

            if (await _reviewRepository.ReviewByUserExists(userId, review.BookId))
            {
                return ServiceResponse.Error($"You have already posted a review for book Id: {review.BookId}", HttpStatusCode.BadRequest);
            }

            var newReview = await _reviewRepository.CreateReview(review);

            return ServiceResponse<ReviewDto>.Success(newReview, "Review created.", HttpStatusCode.Created);
        }

        public async Task<ServiceResponse> GetReviews(int? bookId)
        {
            if (bookId == null)
            {
                return ServiceResponse<IEnumerable<ReviewDto>>.Success(await _reviewRepository.GetReviews(bookId), $"All reviews retrieved.");
            }    

            if (await _bookGetter.GetById<BookDto>((int)bookId) == null)
            {
                return ServiceResponse.Error($"Book with Id: {bookId} doesn't exist", HttpStatusCode.BadRequest);
            }

            return ServiceResponse<IEnumerable<ReviewDto>>.Success(await _reviewRepository.GetReviews(bookId), $"Reviews for Book with ID = {bookId} retrieved.");
        }
    }
}

