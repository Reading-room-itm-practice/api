using AutoMapper;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Search;
using Core.Services;
using Storage.DataAccessLayer;
using Storage.Models;
using System.Linq;

namespace Core.Repositories.Search
{
    class SearchBookRepository : ISearchBookRepository
    {
        private readonly ApiDbContext _context;
        public SearchBookRepository(ApiDbContext context)
        {
            _context = context;
        }
        public DataDto<Book> GetBooks(PaginationFilter filter, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null)
        {
            var searchQueries = AdditionalSearchMethods.ProcessSearchString(searchString);
            var books = _context.Set<Book>().AsEnumerable()
                            .Where(b => AdditionalSearchMethods.ContainsQuery(b.Title, searchQueries)).AsQueryable();

            foreach (string query in searchQueries)
                if (int.TryParse(query, out int year))
                {
                    books = books.AsEnumerable().Where(b => b.RelaseDate.Value.Year == year).AsQueryable();
                }

            if (categoryId != null) books = books.Where(b => b.CategoryId == categoryId);
            if (authorId != null) books = books.Where(b => b.AuthorId == authorId);
            if (minYear != null) books = books.Where(b => b.RelaseDate.Value.Year >= minYear);
            if (maxYear != null) books = books.Where(b => b.RelaseDate.Value.Year <= maxYear);

            books = AdditionalSearchMethods.SortBooks(books, sort);
            return AdditionalSearchMethods.Pagination(filter, books.ToList().AsEnumerable());
        }
    }
}
