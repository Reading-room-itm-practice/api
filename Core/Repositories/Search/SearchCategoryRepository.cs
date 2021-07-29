using AutoMapper;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Search;
using Core.Services;
using Storage.DataAccessLayer;
using Storage.Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.Repositories.Search
{
    class SearchCategoryRepository : ISearchCategoryRepository
    {
        private readonly ApiDbContext _context;
        public SearchCategoryRepository(ApiDbContext context)
        {
            _context = context;
        }
        public DataDto<Category> GetCategories(PaginationFilter filter, string searchString, SortType? sort)
        {
            var searchQueries = AdditionalSearchMethods.ProcessSearchString(searchString);
            var categories = _context.Set<Category>().AsEnumerable()
                .Where(c => AdditionalSearchMethods.ContainsQuery(c.Name, searchQueries)).AsQueryable();

            categories = AdditionalSearchMethods.SortGeneric(categories, sort);

            return AdditionalSearchMethods.Pagination(filter, categories.ToList().AsEnumerable());
        }
    }
}
