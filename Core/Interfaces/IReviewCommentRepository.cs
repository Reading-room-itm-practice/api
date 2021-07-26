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
        public IEnumerable<ReviewCommentDto> GetComments(int? reviewId, Guid? userId);
        public IEnumerable<ReviewCommentDto> GetComments();
        public Task<ReviewCommentDto> GetComment(int reviewCommentId);
        public Task<bool> CheckCommentCount(int reviewId, Guid userId);
        public Task<bool> CheckCommentsDate(int reviewId, Guid userId);
        public Task<bool> ReviewExists(int reviewId);
        public Task<ReviewCommentDto> CreateReviewComment(ReviewCommentRequest newComment);
        public int MaxCommentPerReview { get; }
        public int MaxCommentPerHourPerReview { get; }
    }
}
