using Storage.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        public Task<bool> ReviewByUserExists(Guid userId, int bookId);
        public Task<Review> CreateReview(Review review);
    }
}
