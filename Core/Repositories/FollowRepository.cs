using System;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models.Follows;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class FollowRepository<T> : BaseRepository<T> where T : Follow
    {
        public FollowRepository(ApiDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<T>> FindByConditions(Expression<Func<T, bool>> expresion)
        {
            IQueryable<T> follows = _context.Set<T>();
            if (typeof(T) == typeof(AuthorFollow))
            {
                follows = follows.Include("Author").Include("Author.MainPhoto");
            }
            if (typeof(T) == typeof(CategoryFollow))
            {
                return (IEnumerable<T>) await _context.CategoryFollows.Include(a => a.Category).ToListAsync();
            }
            if (typeof(T) == typeof(UserFollow))
            {
                return (IEnumerable<T>) await _context.UserFollows.Include(a => a.Following).ThenInclude(p => p.ProfilePhoto).ToListAsync();
            }

            return await follows.ToListAsync();
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
            => _context.AuthorFollows
                .Where(c => c.CreatorId == model.CreatorId)
                .FirstOrDefault(a => a.AuthorId == model.AuthorId) != null;

        private bool IsCategoryFollowExists(CategoryFollow model)
            => _context.CategoryFollows
                .Where(c => c.CreatorId == model.CreatorId)
                .FirstOrDefault(a => a.CategoryId == model.CategoryId) != null;

        private bool IsUserFollowExists(UserFollow model)
            => _context.UserFollows
                .Where(c => c.CreatorId == model.CreatorId)
                .FirstOrDefault(a => a.FollowingId == model.FollowingId) != null;
    }
}
