using AutoMapper;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Search;
using Core.Services;
using Storage.DataAccessLayer;
using System.Collections.Generic;
using System.Linq;

namespace Core.Repositories.Search
{
    class SearchCategoryRepository : ISearchCategoryRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        public SearchCategoryRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public DataDto<CategoryDto> GetCategories(PaginationFilter filter, string searchString, SortType? sort)
        {
            var searchQueries = AdditionalSearchMethods.ProcessSearchString(searchString);
            var categories = (_mapper.Map<IEnumerable<CategoryDto>>(_context.Categories))
                .Where(c => AdditionalSearchMethods.ContainsQuery(c.Name, searchQueries));

            categories = AdditionalSearchMethods.SortGeneric(categories, sort);
            return AdditionalSearchMethods.Pagination(filter, categories);
        }
    }
}
