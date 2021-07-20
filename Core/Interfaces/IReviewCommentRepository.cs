using Core.DTOs;
using Core.Requests;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReviewCommentRepository : IBaseRepository<ReviewComment>
    {
        public Task<IEnumerable<ReviewCommentDto>> GetComments(int? reviewId, int? userId);
        public Task<IEnumerable<ReviewCommentDto>> GetComments();
        public Task<bool> CheckCommentCount(int reviewId, int userId);
        public Task<bool> CheckCommentsDate(int reviewId, int userId);
        public Task<ReviewCommentDto> CreateReviewComment(ReviewCommentRequest newComment);
        public int MaxCommentPerReview { get; }
        public int MaxCommentPerHourPerReview { get; }

    }
}
