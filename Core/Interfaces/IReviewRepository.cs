using Core.DTOs;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        public Task<IEnumerable<ReviewDto>> GetReviews(int? bookId);
        public Task<bool> ReviewByUserExists(int userId, int bookId);
    }
}
