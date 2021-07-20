using Core.Requests;
using Core.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReviewCommentService
    {
        public Task<ServiceResponse> GetComments(int? reviewId, int? userId, bool? currentUser);

        public Task<ServiceResponse> AddReviewComment(ReviewCommentRequest comment);
    }
}
