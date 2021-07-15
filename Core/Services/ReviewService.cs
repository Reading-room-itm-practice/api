using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.ServiceResponses;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
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
