using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Interfaces;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IGetterService<Book> _bookGetter;
        private readonly ILoggedUserProvider _loggedUserProvider;

        public ReviewService(IReviewRepository reviewRepository, IGetterService<Book> bookGetter, ILoggedUserProvider loggedUserProvider)
        {
            _loggedUserProvider = loggedUserProvider;
            _reviewRepository = reviewRepository;
            _bookGetter = bookGetter;
        }
        public async Task<ServiceResponse> AddReview(ReviewRequest review)
        {
            var userId = _loggedUserProvider.GetUserId();

            if (!(await BookExists(review.BookId))) return new ErrorResponse()
            {
                Message = $"Book with Id: {review.BookId} doesn't exist",
                StatusCode = HttpStatusCode.BadRequest
            };

            if (await _reviewRepository.ReviewByUserExists(userId, review.BookId)) return new ErrorResponse()
            {
                Message = $"User with Id: {userId} has already posted a review for book Id: {review.BookId}",
                StatusCode = HttpStatusCode.BadRequest
            };

            var newReview = await _reviewRepository.CreateReview(review);
            return new SuccessResponse<ReviewDto>()
            { 
                Message = "Review created.", 
                StatusCode = HttpStatusCode.Created, 
                Content = newReview 
            };

        }
        private async Task<bool> BookExists(int bookId)
        {
            if (await _bookGetter.GetById<BookDto>(bookId) == null) return false;
            return true;
        }

        public async Task<ServiceResponse> GetReviews(int? bookId)
        {
            if(bookId == null) return new SuccessResponse<IEnumerable<ReviewDto>>()
            {
                Message = $"All reviews retrieved.",
                Content = await _reviewRepository.GetReviews(bookId)
            };

            return new SuccessResponse<IEnumerable<ReviewDto>>()
            {
                Message = $"Reviews for Book with ID = {bookId} retrieved.",
                Content = await _reviewRepository.GetReviews(bookId)
            };
        }

    }
}
