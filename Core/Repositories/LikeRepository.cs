using Core.Exceptions;
using Storage.DataAccessLayer;
using Storage.Models.Likes;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class LikeRepository<T> : BaseRepository<T> where T : Like
    {
        public LikeRepository(ApiDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<T> Create(T model)
        {
            if (IsLikeExist(model)) 
            {
                throw new ResourceExistsException("Resource already exists");
            }
            _context.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        private bool IsLikeExist(Like model)
        {
            return model switch
            {
                ReviewLike reviewLike => IsReviewLikeExist(reviewLike),
                ReviewCommentLike commentLike => IsCommentLikeExist(commentLike),
                _ => false
            };
        }

        private bool IsReviewLikeExist(ReviewLike model)
        {
            return _context.ReviewLikes.Where(c => c.CreatorId == model.CreatorId)
                .FirstOrDefault(a => a.ReviewId == model.ReviewId) != null;
        }

        private bool IsCommentLikeExist(ReviewCommentLike model)
        {
            return _context.CommentLikes.Where(c => c.CreatorId == model.CreatorId)
                .FirstOrDefault(a => a.ReviewCommentId == model.ReviewCommentId) != null;
        }
    }
}
