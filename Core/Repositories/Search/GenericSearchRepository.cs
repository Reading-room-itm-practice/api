using Core.DTOs;
using Core.Enums;
using Core.Services;
using Storage.DataAccessLayer;
using Storage.Identity;
using Storage.Interfaces;
using Storage.Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.Repositories.Search
{
    internal interface IGenericSearchRepository
    {
        public DataDto<T> GetEntities<T>(PaginationFilter filter, string searchString, SortType? sort) where T : class;
    }
    class GenericSearchRepository : IGenericSearchRepository
    {
        private readonly ApiDbContext _context;

        public GenericSearchRepository(ApiDbContext context)
        {
            _context = context;
        }
        //noDto
        public DataDto<T> GetEntities<T>(PaginationFilter filter, string searchString, SortType? sort) where T : class
        {

            var searchQueries = AdditionalSearchMethods.ProcessSearchString(searchString);
            var entities = _context.Set<T>().AsEnumerable();

            if (typeof(T) == typeof(Category))
            {
                var cat = entities as IEnumerable<Category>;
                entities = (IEnumerable<T>)cat.
                    Where(u => AdditionalSearchMethods.ContainsQuery(u.Name, searchQueries)).AsQueryable();
            }
            else if (typeof(T) == typeof(Author))
            {
                var auth = entities as IEnumerable<Author>;
                entities = (IEnumerable<T>)auth.
                    Where(u => AdditionalSearchMethods.ContainsQuery(u.Name, searchQueries)).AsQueryable();
            }
            else if (typeof(T) == typeof(User))
            {
                var user = entities as IEnumerable<User>;
                entities = (IEnumerable<T>)user.
                    Where(u => AdditionalSearchMethods.ContainsQuery(u.UserName, searchQueries)).AsQueryable();
            }

            entities = AdditionalSearchMethods.SortGeneric(entities.AsQueryable(), sort);

            return AdditionalSearchMethods.Pagination(filter, entities.ToList().AsEnumerable());
        }
        //noDto
    }
}
