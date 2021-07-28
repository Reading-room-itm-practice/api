using Core.Exceptions;
using Core.Interfaces;
using Storage.DataAccessLayer;
using Storage.Models.Follows;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class FollowRepository<T> : BaseRepository<T>, IBaseRepository<T> where T : Follow
    {
        public FollowRepository(ApiDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<T> Create(T model)
        {
            if(IsFollowExists(model))
            {
                throw new ResourceExistsException("Resource already exists");
            }
            _context.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        private bool IsFollowExists(Follow model)
            => model switch
                {
                    AuthorFollow authorFollow => IsAuthorFollowExists(authorFollow),
                    CategoryFollow categoryFollow => IsCategoryFollowExists(categoryFollow),
                    UserFollow userFollow => IsUserFollowExists(userFollow),
                    _ => false
                };

        private bool IsAuthorFollowExists(AuthorFollow model)
            => _context.AuthorFollows.Where(c => c.CreatorId == model.CreatorId)
                    .Where(a => a.AuthorId == model.AuthorId)
                    .FirstOrDefault() != null;

        private bool IsCategoryFollowExists(CategoryFollow model)
            => _context.CategoryFollows.Where(c => c.CreatorId == model.CreatorId)
                    .Where(a => a.CategoryId == model.CategoryId)
                    .FirstOrDefault() != null;

        private bool IsUserFollowExists(UserFollow model)
            => _context.UserFollows.Where(c => c.CreatorId == model.CreatorId)
                    .Where(a => a.FollowingId == model.FollowingId)
                    .FirstOrDefault() != null;
    }
}
