using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Search;
using Core.Services;
using Storage.DataAccessLayer;
using Storage.Models;
using System.Linq;

namespace Core.Repositories.Search
{
    class SearchAuthorRepository : ISearchAuthorRepository
    {
        private readonly ApiDbContext _context;

        public SearchAuthorRepository(ApiDbContext context)
        {
            _context = context;
        }

        public DataDto<Author> GetAuthors(PaginationFilter filter, string searchString, SortType? sort)
        {
            var searchQueries = AdditionalSearchMethods.ProcessSearchString(searchString);
            var authors = _context.Set<Author>().AsEnumerable()
                .Where(a => AdditionalSearchMethods.ContainsQuery(a.Name, searchQueries)).AsQueryable();

            authors = AdditionalSearchMethods.SortGeneric(authors, sort);

            return AdditionalSearchMethods.Pagination(filter, authors.ToList().AsEnumerable());
        }
    }
}
