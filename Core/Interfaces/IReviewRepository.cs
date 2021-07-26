using Core.DTOs;
using Core.Requests;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        public Task<IEnumerable<ReviewDto>> GetReviews(int? bookId);
        public Task<bool> ReviewByUserExists(Guid userId, int bookId);
        public Task<ReviewDto> CreateReview(ReviewRequest review);
        public Task<ReviewDto> GetReview(int reviewId);
    }
}
