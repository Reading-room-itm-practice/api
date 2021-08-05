using Core.Exceptions;
using Storage.DataAccessLayer;
using Storage.Models.Follows;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class FollowsRepository<T> : BaseRepository<T> where T : Follow
    {
        public FollowsRepository(ApiDbContext dbContext) : base(dbContext)
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
        {
            return model switch
            {
                AuthorFollow authorFollow => IsAuthorFollowExists(authorFollow),
                CategoryFollow categoryFollow => IsCategoryFollowExists(categoryFollow),
                UserFollow userFollow => IsUserFollowExists(userFollow),
                _ => false
            };
        }

        private bool IsAuthorFollowExists(AuthorFollow model)
        {
            return _context.AuthorFollows.Where(c => c.CreatorId == model.CreatorId)
                .FirstOrDefault(a => a.AuthorId == model.AuthorId) != null;
        }

        private bool IsCategoryFollowExists(CategoryFollow model)
        {
            return _context.CategoryFollows.Where(c => c.CreatorId == model.CreatorId)
                .FirstOrDefault(a => a.CategoryId == model.CategoryId) != null;
        }

        private bool IsUserFollowExists(UserFollow model)
        {
            return _context.UserFollows.Where(c => c.CreatorId == model.CreatorId)
                .FirstOrDefault(a => a.FollowingId == model.FollowingId) != null;
        }
    }
}
