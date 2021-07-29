using Core.DTOs;
using Core.Enums;
using Core.Services;
using Storage.DataAccessLayer;
using Storage.Identity;
using Storage.Interfaces;
using Storage.Models;
using System.Linq;

namespace Core.Repositories.Search
{
    internal interface IGenericSearchRepository
    {
        public DataDto<T> GetEntities<T>(PaginationFilter filter, string searchString, SortType? sort);
    }
    class GenericSearchRepository : IGenericSearchRepository
    {
        private readonly ApiDbContext _context;

        public GenericSearchRepository(ApiDbContext context)
        {
            _context = context;
        }

        public DataDto<T> GetEntities<T>(PaginationFilter filter, string searchString, SortType? sort) where T : class, ISearchable
        {
            var searchQueries = AdditionalSearchMethods.ProcessSearchString(searchString);
            var entities = _context.Set<T>().AsEnumerable().
                Where(u => AdditionalSearchMethods.ContainsQuery(u.UserName, searchQueries)).AsQueryable();

            entities = AdditionalSearchMethods.SortUsers(entities, sort);

            return AdditionalSearchMethods.Pagination(filter, entities.ToList().AsEnumerable());
        }
    }
}
