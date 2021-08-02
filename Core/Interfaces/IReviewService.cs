using Core.Requests;
using Core.Response;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReviewService
    {
        public Task<ServiceResponse> AddReview(ReviewRequest review);
    }
}
