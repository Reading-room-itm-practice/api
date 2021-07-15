using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<IEnumerable<ReviewDto>> GetReviews(int? bookId)
        {
            if(bookId != null && await _context.Books.AnyAsync(b => b.Id == bookId))
                return _mapper.Map<IEnumerable<ReviewDto>>((await _context.Books.Include(b => b.Reviews)
                    .FirstOrDefaultAsync(b => b.Id == bookId)).Reviews.AsEnumerable());

            return _mapper.Map<IEnumerable<ReviewDto>>(await FindAll());
        }
    }
}
