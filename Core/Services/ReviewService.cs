﻿using AutoMapper;
using Core.Common;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
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
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly IGetterService<Book> _bookGetter;
        private readonly ILoggedUserProvider _loggedUserProvider;

        public ReviewService(IReviewRepository reviewRepository, IGetterService<Book> bookGetter, 
            ILoggedUserProvider loggedUserProvider, IMapper mapper)
        {
            _mapper = mapper;
            _loggedUserProvider = loggedUserProvider;
            _reviewRepository = reviewRepository;
            _bookGetter = bookGetter;
        }

        public async Task<ServiceResponse> AddReview(ReviewRequest review)
        {
            if ((await _bookGetter.GetById<BookDto>(review.BookId)).Content == null)
                return ServiceResponse.Error($"The book you are trying to post a review for doesn't exist", HttpStatusCode.BadRequest);

            var userId = _loggedUserProvider.GetUserId();
            if (await _reviewRepository.ReviewByUserExists(userId, review.BookId)) return ServiceResponse.Error
                    ($"You have already posted a review for {await GetBookTitle(review.BookId)}", HttpStatusCode.BadRequest);

            var newReview = await _reviewRepository.CreateReview(_mapper.Map<Review>(review));
            return ServiceResponse<ReviewDto>.Success(_mapper.Map<ReviewDto>(newReview), "Review created.", HttpStatusCode.Created);
        }

        public async Task<ServiceResponse> GetReviews(int? bookId)
        {
            if (bookId == null) return ServiceResponse<IEnumerable<ReviewDto>>.Success
                    (_mapper.Map<IEnumerable<ReviewDto>>(await _reviewRepository.GetReviews(bookId)), $"All reviews retrieved.");

            if ((await _bookGetter.GetById<BookDto>((int)bookId)).Content == null)
                return ServiceResponse.Error($"The book you are trying to find a review for doesn't exist", HttpStatusCode.NotFound);

            return ServiceResponse<IEnumerable<ReviewDto>>.Success
                    (_mapper.Map<IEnumerable<ReviewDto>>(await _reviewRepository.GetReviews(bookId)), 
                        $"Reviews for {await GetBookTitle((int)bookId)} retrieved.");
        }

        public async Task<ServiceResponse> GetReview(int reviewId)
        {
            var review = await _reviewRepository.GetReview(reviewId);
            if (review != null) return ServiceResponse<ReviewDto>.Success (_mapper.Map<ReviewDto>(review), $"Review retrieved.");
            return ServiceResponse.Error("Review not found.", HttpStatusCode.NotFound);
        }

        private async Task<string> GetBookTitle(int bookId)
        {
            return (await _bookGetter.GetById<BookDto>(bookId)).Content.Title;
        }
        
    }
}

