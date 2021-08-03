using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Interfaces;

namespace Core.Repositories
{
    public class ExtendedBaseRepository<T> : BaseRepository<T>, IExtendedBaseRepository<T> where T : class, IDbModel
    {
        public ExtendedBaseRepository(ApiDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<T>> FindByConditionsWithIncludes(Expression<Func<T, bool>> expression, params string[] includes)
        {
            IQueryable<T> models = _context.Set<T>().Where(expression);

            foreach (var includeEntity in includes)
            {
                models = models.Include(includeEntity);
            }

            return await models.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllWithIncludes(params string[] includes)
        {
            IQueryable<T> models = _context.Set<T>();

            foreach (var includeEntity in includes)
            {
                models = models.Include(includeEntity);
            }

            return await models.ToListAsync();
        }
    }
}
