using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Storage.DataAccessLayer;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class ReviewCommentRepository : BaseRepository<ReviewComment>, IReviewCommentRepository
    {
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;
        public int MaxCommentPerReview { get; }
        public int MaxCommentPerHourPerReview { get; }
        
        public ReviewCommentRepository(IConfiguration configuration, IMapper mapper, ApiDbContext context) : base(context)
        {
            this.configuration = configuration;
            _mapper = mapper;
            MaxCommentPerReview = int.Parse(configuration["MaxCommentPerReview"]);
            MaxCommentPerHourPerReview = int.Parse(configuration["MaxCommentPerHourPerReview"]);
        }

        public async Task<bool> CheckCommentCount(int reviewId, Guid userId)
        {
            var review = await _context.Reviews.Include(r => r.Comments).FirstOrDefaultAsync(r => r.Id == reviewId);
            return review.Comments.Where(c => c.CreatorId == userId).Count() >= MaxCommentPerReview;
        }

        public async Task<bool> CheckCommentsDate(int reviewId, Guid userId)
        {
            var review = (await _context.Reviews.Include(r => r.Comments).FirstOrDefaultAsync(r => r.Id == reviewId));
            return review.Comments.Where(c => c.CreatorId == userId && 
                (DateTime.Now.Subtract(c.CreatedAt)).Hours < 1).Count() >= MaxCommentPerHourPerReview;
        }

        public async Task<ReviewCommentDto> CreateReviewComment(ReviewCommentRequest _newComment)
        {
            var newComment = _mapper.Map<ReviewComment>(_newComment);
            return await GetComment((await Create(newComment)).Id);
        }

        public IEnumerable<ReviewCommentDto> GetComments()
        {
            return _mapper.Map<IEnumerable<ReviewCommentDto>>(_context.ReviewComments.Include(c => c.Creator));
        }

        public IEnumerable<ReviewCommentDto> GetComments(int? reviewId, Guid? userId)
        {
            if (reviewId == null && userId == null) return GetComments();
            var comments = _context.ReviewComments.Include(c => c.Creator).AsEnumerable();

            if (reviewId == null && userId != null)
                return _mapper.Map<IEnumerable<ReviewCommentDto>>(comments.Where(c => c.CreatorId == userId));

            comments = comments.Where(c => c.ReviewId == reviewId);
            comments = (userId == null) ? comments : comments.Where(c => c.CreatorId == userId);
            return _mapper.Map<IEnumerable<ReviewCommentDto>>(comments);
        }

        public async Task<ReviewCommentDto> GetComment(int reviewCommentId)
        {
            return _mapper.Map<ReviewCommentDto>(await _context.ReviewComments.Include(c => c.Creator)
                .FirstOrDefaultAsync(c => c.Id == reviewCommentId));
        }

        public async Task<bool> ReviewExists(int reviewId)
        {
            return await _context.Reviews.AnyAsync(r => r.Id == reviewId);
        }
    }
}