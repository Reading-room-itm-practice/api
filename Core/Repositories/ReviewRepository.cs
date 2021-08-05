using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApiDbContext context) : base(context) { }
      
        public async Task<IEnumerable<Review>> GetReviews(int? bookId)
        {
            var reviews = _context.Reviews.Include(r => r.Creator);
            if (bookId != null && await _context.Books.AnyAsync(b => b.Id == bookId)) return reviews.Where(r => r.BookId == bookId);
            return reviews;
        }

        public async Task<bool> ReviewByUserExists(Guid userId, int bookId)
        {
            var book = await _context.Books.Include(b => b.Reviews).FirstOrDefaultAsync(b => b.Id == bookId);
            return book.Reviews.Any(r => r.CreatorId == userId);
        }

        public async Task<Review> CreateReview(Review reviewRequest)
        {
            return await GetReview((await Create(reviewRequest)).Id);
        }
        public async Task<Review> GetReview(int reviewId)
        {
            return await _context.Reviews.Include(r => r.Creator).FirstOrDefaultAsync(r => r.Id == reviewId);
        }
    }
}
