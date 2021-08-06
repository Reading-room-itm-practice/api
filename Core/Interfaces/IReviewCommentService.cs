using Core.Requests;
using Core.Response;
using System;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReviewCommentService
    {
        public Task<ServiceResponse> GetComments(int? reviewId, Guid? userId, bool currentUser);
        public Task<ServiceResponse> GetComment(int reviewCommentId);
        public Task<ServiceResponse> AddReviewComment(ReviewCommentRequest comment);
    }
}
