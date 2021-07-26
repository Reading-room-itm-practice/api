using Core.Interfaces;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Iterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IDbModel
    {
        protected readonly ApiDbContext _context;

        public BaseRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }
        public async Task Delete(T model)
        {
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(T model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindAll(PaginationFilter filter)
        {
            filter.Valid();
            if (filter.PageSize != 0)
            {
                return await _context.Set<T>()
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
            }

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditions(Expression<Func<T, bool>> expresion)
        {
            return await _context.Set<T>().Where(expresion).ToListAsync();
        }
    }
}
