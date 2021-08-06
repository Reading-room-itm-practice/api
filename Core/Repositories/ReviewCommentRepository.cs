using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Storage.DataAccessLayer;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class ReviewCommentRepository : BaseRepository<ReviewComment>, IReviewCommentRepository
    {
        public int MaxCommentPerReview { get; }
        public int MaxCommentPerHourPerReview { get; }
        
        public ReviewCommentRepository(IConfiguration configuration, ApiDbContext context) : base(context)
        {
            MaxCommentPerReview = int.Parse(configuration["MaxCommentPerReview"]);
            MaxCommentPerHourPerReview = int.Parse(configuration["MaxCommentPerHourPerReview"]);
        }

        public async Task<bool> HitCommentCapCount(int reviewId, Guid userId)
        {
            var review = await _context.Reviews.Include(r => r.Comments).FirstOrDefaultAsync(r => r.Id == reviewId);
          
            return review.Comments.Count(c => c.CreatorId == userId) >= MaxCommentPerReview;
        }

        public async Task<bool> HitCommentCapDate(int reviewId, Guid userId)
        {
            var review = (await _context.Reviews.Include(r => r.Comments).FirstOrDefaultAsync(r => r.Id == reviewId));
          
            return review.Comments.Count(c => c.CreatorId == userId && (DateTime.Now.Subtract(c.CreatedAt)).Hours < 1) >= MaxCommentPerHourPerReview;
        }

        public override async Task<ReviewComment> Create(ReviewComment commentModel)
        {
            _context.Add(commentModel);
            await _context.SaveChangesAsync();

            return await GetComment((commentModel).Id);
        }

        public IEnumerable<ReviewComment> GetComments()
        {
            return _context.ReviewComments.Include(c => c.Creator);
        }

        public IEnumerable<ReviewComment> GetComments(int? reviewId, Guid? userId)
        {
            if (reviewId == null && userId == null)
            {
                return GetComments();
            }
            var comments = _context.ReviewComments
                .Include(c => c.Creator).Include(l=> l.Likes).AsEnumerable();

            if (reviewId == null)
            {
                return comments.Where(c => c.CreatorId == userId);
            }

            comments = comments.Where(c => c.ReviewId == reviewId);
            comments = (userId == null) ? comments : comments.Where(c => c.CreatorId == userId);
            
            return comments;
        }

        public async Task<ReviewComment> GetComment(int reviewCommentId)
        {
            return await _context.ReviewComments.Include(c => c.Creator)
                .Include(l => l.Likes).FirstOrDefaultAsync(c => c.Id == reviewCommentId);
        }

        public async Task<bool> ReviewExists(int reviewId)
        {
            return await _context.Reviews.AnyAsync(r => r.Id == reviewId);
        }
    }
}