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
        public IEnumerable<ReviewComment> GetComments(int? reviewId, Guid? userId);
        public IEnumerable<ReviewComment> GetComments();
        public Task<ReviewComment> GetComment(int reviewCommentId);
        public Task<bool> HitCommentCapCount(int reviewId, Guid userId);
        public Task<bool> HitCommentCapDate(int reviewId, Guid userId);
        public Task<bool> ReviewExists(int reviewId);
        public int MaxCommentPerReview { get; }
        public int MaxCommentPerHourPerReview { get; }
    }
}
