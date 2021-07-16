using Core.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReviewService
    {
        public Task<ServiceResponse> GetReviews(int? bookId);
        public Task<bool> ReviewByUserExists(int userId, int bookId);
    }
}
