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
    class SearchBookRepository : ISearchBookRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        public SearchBookRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public DataDto<BookDto> GetBooks(PaginationFilter filter, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null)
        {
            var searchQueries = AdditionalSearchMethods.ProcessSearchString(searchString);
            var books = (_mapper.Map<IEnumerable<BookDto>>(_context.Books))
                            .Where(b => AdditionalSearchMethods.ContainsQuery(b.Title, searchQueries));

            foreach (string query in searchQueries)
                if (int.TryParse(query, out int year))
                {
                    var booksByYear = (_mapper.Map<IEnumerable<BookDto>>(_context.Books))
                            .Where(b => b.ReleaseYear == year);
                    books = books.Concat(booksByYear);
                }

            if (categoryId != null) books = books.Where(b => b.CategoryId == categoryId);
            if (authorId != null) books = books.Where(b => b.AuthorId == authorId);
            if (minYear != null) books = books.Where(b => b.ReleaseYear >= minYear);
            if (maxYear != null) books = books.Where(b => b.ReleaseYear <= maxYear);

            books = AdditionalSearchMethods.SortBooks(books, sort);
            return AdditionalSearchMethods.Pagination(filter, books);
        }
    }
}
