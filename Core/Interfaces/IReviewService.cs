using Core.Requests;
using Core.Response;
using Core.Services;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReviewService
    {
        public Task<ServiceResponse> GetReviews(PaginationFilter filter, string route);
        public Task<ServiceResponse> AddReview(ReviewRequest review);
    }
}
