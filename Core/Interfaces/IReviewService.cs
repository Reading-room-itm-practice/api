using Core.Requests;
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
        public Task<ServiceResponse> GetReview(int reviewId);
        public Task<ServiceResponse> AddReview(ReviewRequest review);
    }
}
