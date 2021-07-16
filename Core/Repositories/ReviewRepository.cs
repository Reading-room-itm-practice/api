using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Identity;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public ReviewRepository(UserManager<User> userManager, IMapper mapper, ApiDbContext context) : base(context)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ReviewDto>> GetReviews(int? bookId)
        {
            if(bookId != null && await _context.Books.AnyAsync(b => b.Id == bookId))
                return _mapper.Map<IEnumerable<ReviewDto>>((await _context.Books.Include(b => b.Reviews)
                    .FirstOrDefaultAsync(b => b.Id == bookId)).Reviews.AsEnumerable());

            return _mapper.Map<IEnumerable<ReviewDto>>(await FindAll());
        }
        public async Task<bool> ReviewByUserExists(int userId, int bookId)
        {
            //var uu = await _context.Users.Include(u => u.Reviews).FirstOrDefaultAsync(u => u.Id == 1);
            //if (!(int.TryParse(_userId, out int userId))) return true;

            var userReview = await _context.Users.Include(u => u.Reviews).FirstOrDefaultAsync(u => u.Id == userId);
            if (userReview.Reviews.Count() != 0) return true;
            return false;
        }
    }
}
