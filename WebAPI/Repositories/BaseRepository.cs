using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;
using WebAPI.DataAccessLayer;
using WebAPI.Interfaces;

namespace WebAPI.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        protected readonly ApiDbContext _context;

        public BaseRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<T> Create<T>(T model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task Delete<T>(T model)
        {
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task Edit<T>(T model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
