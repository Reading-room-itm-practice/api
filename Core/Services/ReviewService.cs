using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Identity;
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
            if (await _bookGetter.GetById<BookDto>(review.BookId) == null) return new ErrorResponse()
            {
                Message = $"Book doesn't exist",
                StatusCode = HttpStatusCode.BadRequest
            };

            var userId = _loggedUserProvider.GetUserId();
            if (await _reviewRepository.ReviewByUserExists(userId, review.BookId)) return new ErrorResponse()
            {
                Message = $"You have already posted a review for {await GetBookTitle(review.BookId)}",
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

        public async Task<ServiceResponse> GetReviews(int? bookId)
        {
            if (bookId == null) return new SuccessResponse<IEnumerable<ReviewDto>>()
            {
                Message = $"All reviews retrieved.",
                Content = await _reviewRepository.GetReviews(bookId)
            };

            if (await _bookGetter.GetById<BookDto>((int)bookId) == null) return new ErrorResponse()
            {
                Message = $"Book doesn't exist",
                StatusCode = HttpStatusCode.BadRequest
            };

            return new SuccessResponse<IEnumerable<ReviewDto>>()
            {
                Message = $"Reviews for {await GetBookTitle((int)bookId)} retrieved.",
                Content = await _reviewRepository.GetReviews(bookId)
            };
        }

        private async Task<string> GetBookTitle(int bookId)
        {
            var book = await _bookGetter.GetById<BookDto>(bookId);
            return book.Title;
        }
    }
}

