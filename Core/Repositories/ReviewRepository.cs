using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApiDbContext context) : base(context) {}

        public async Task<bool> ReviewByUserExists(Guid userId, int bookId)
        {
            var book = await _context.Books.Include(b => b.Reviews).FirstOrDefaultAsync(b => b.Id == bookId);
            return book.Reviews.Any(r => r.CreatorId == userId);
        }

        public async Task<Review> CreateReview(Review review)
        {
            return await Create(review);
        }
    }
}
