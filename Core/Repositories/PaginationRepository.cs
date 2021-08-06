using Core.Common;
using Core.DTOs;
using Core.Interfaces;
using Core.Services;
using Core.Services.Search;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    class PaginationRepository : IPaginationRepository
    {
        protected readonly ApiDbContext _context;

        public PaginationRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<ExtendedData<IEnumerable<T>>> FindAll<T>(PaginationFilter filter) where T : class
        {
            var totalRecords = await _context.Set<T>().CountAsync();
            var data = _context.Set<T>().AsQueryable();
            if (filter.PageSize != 0)
            {
                data = data
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);
                
            }

            return new ExtendedData<IEnumerable<T>>()
            {
                Data = await data.ToListAsync(),
                Quantity = totalRecords
            };
        }
    }
}
