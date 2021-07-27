using AutoMapper;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Search;
using Core.Services;
using Storage.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Repositories.Search
{
    class SearchAuthorRepository : ISearchAuthorRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        public SearchAuthorRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public DataDto<AuthorDto> GetAuthors(PaginationFilter filter, string searchString, SortType? sort)
        {
            var searchQueries = AdditionalSearchMethods.ProcessSearchString(searchString);
            var authors = (_mapper.Map<IEnumerable<AuthorDto>>(_context.Authors))
                .Where(a => AdditionalSearchMethods.ContainsQuery(a.Name, searchQueries));

            authors = AdditionalSearchMethods.SortGeneric(authors, sort);

            return AdditionalSearchMethods.Pagination(filter, authors);
        }
    }
}
