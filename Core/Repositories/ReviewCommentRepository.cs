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

        public async Task<bool> CheckCommentCount(int reviewId, int userId)
        {
            var review = await _context.Reviews.Include(r => r.Comments).FirstOrDefaultAsync(r => r.Id == reviewId);
            return review.Comments.Where(c => c.CreatedBy == userId).Count() >= MaxCommentPerReview;
        }

        public async Task<bool> CheckCommentsDate(int reviewId, int userId)
        {
            var review = (await _context.Reviews.Include(r => r.Comments).FirstOrDefaultAsync(r => r.Id == reviewId));
            return review.Comments.Where(c => c.CreatedBy == userId && 
                (DateTime.Now.Subtract(c.Created)).Hours < 1).Count() >= MaxCommentPerHourPerReview;
        }

        public async Task<ReviewCommentDto> CreateReviewComment(ReviewCommentRequest _newComment)
        {
            var newComment = _mapper.Map<ReviewComment>(_newComment);
            await Create(newComment);
            return _mapper.Map<ReviewCommentDto>(newComment);
        }

        public async Task<IEnumerable<ReviewCommentDto>> GetComments()
        {
            return _mapper.Map<IEnumerable<ReviewCommentDto>>(await FindAll());
        }

        public async Task<IEnumerable<ReviewCommentDto>> GetComments(int? reviewId, int? userId)
        {
            if (reviewId == null && userId == null) return await GetComments();

            if (reviewId == null && userId != null)
                return _mapper.Map<IEnumerable<ReviewCommentDto>>(_context.ReviewComments.Where(c => c.CreatedBy == userId));

            var comments = _context.Reviews.Include(r => r.Comments).FirstOrDefault(r => r.Id == reviewId).Comments.AsEnumerable();
            comments = (reviewId != null && userId == null) ? comments : comments.Where(c => c.CreatedBy == userId);
            return _mapper.Map<IEnumerable<ReviewCommentDto>>(comments);
        }
    }
}