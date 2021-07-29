using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.Services;
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
        private readonly IMapper _mapper;
        public ReviewRepository(IMapper mapper, ApiDbContext context) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<DataDto<Review>> GetReviews(PaginationFilter filter)
        {
            var revievs = await FindAll(filter);
            return revievs;
        }

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
