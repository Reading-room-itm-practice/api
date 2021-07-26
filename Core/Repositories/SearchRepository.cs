using AutoMapper;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces;
using Core.Services;
using Storage.DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebAPI.DTOs;

namespace Core.Repositories
{
    class SearchRepository : ISearchRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        public SearchRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<AuthorDto> GetAuthors(PaginationFilter filter, string searchString, SortType? sort)
        {
            var searchQueries = ProcessSearchString(searchString);
            var authors = (_mapper.Map<IEnumerable<AuthorDto>>(_context.Authors))
                .Where(a => ContainsQuery(a.Name, searchQueries));

            authors = authors.Distinct();
            authors = SortGeneric(authors, sort);
            return Pagination<AuthorDto>(filter, authors);
        }

        public IEnumerable<CategoryDto> GetCategories(PaginationFilter filter, string searchString, SortType? sort)
        {
            var searchQueries = ProcessSearchString(searchString);
            var categories = (_mapper.Map<IEnumerable<CategoryDto>>(_context.Categories))
                .Where(c => ContainsQuery(c.Name, searchQueries));

            categories = categories.Distinct();
            categories = SortGeneric(categories, sort);
            return Pagination<CategoryDto>(filter, categories);
        }

        public IEnumerable<BookDto> GetBooks(PaginationFilter filter, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null)
        {
            var searchQueries = ProcessSearchString(searchString);
            var books = (_mapper.Map<IEnumerable<BookDto>>(_context.Books))
                            .Where(b => ContainsQuery(b.Title, searchQueries));

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
            books = books.Distinct();
            books = SortBooks(books, sort);
            return Pagination<BookDto>(filter, books);
        }

        public IEnumerable<UserSearchDto> GetUsers(PaginationFilter filter, string searchString, SortType? sort)
        {
            var searchQueries = ProcessSearchString(searchString);
            var users = GetUsers(filter, searchQueries);
            users = users.Distinct();
            users = SortUsers(users, sort);
            return Pagination<UserSearchDto>(filter, users);
        }

        private IEnumerable<UserSearchDto> GetUsers(PaginationFilter filter, string[] searchQueries)
        {
            var users = (_mapper.Map<IEnumerable<UserSearchDto>>(_context.Users))
                            .Where(u => ContainsQuery(u.UserName, searchQueries));
            return Pagination<UserSearchDto>(filter, users);
        }

        private bool ContainsQuery(string name, string[] queries)
        {
            if (queries.All(q => name.ToUpper().Contains(q.ToUpper()))) return true;
            return false;
        }

        private string[] ProcessSearchString(string searchString)
        {
            searchString ??= "";
            searchString = Regex.Replace(searchString, @"\s+", " ");
            return searchString.Split(" ");
        }

        private IEnumerable<T> SortGeneric<T>(IEnumerable<T> sorted, SortType? sort) where T : INameSortable
        {
            switch (sort)
            {
                default:
                case SortType.ByName:
                    sorted = sorted.OrderBy(a => a.Name);
                    break;
                case SortType.ByNameDescending:
                    sorted = sorted.OrderByDescending(a => a.Name);
                    break;
            }
            return sorted;
        }

        private IEnumerable<BookDto> SortBooks(IEnumerable<BookDto> books, SortType? sort)
        {
            switch (sort)
            {
                default:
                case SortType.ByName:
                    books = books.OrderBy(b => b.Title);
                    break;
                case SortType.ByNameDescending:
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case SortType.ByRelaseYear:
                    books = books.OrderBy(b => b.ReleaseYear);
                    break;
                case SortType.ByRelaseYearDescending:
                    books = books.OrderByDescending(b => b.ReleaseYear);
                    break;
            }
            return books;
        }

        private IEnumerable<UserSearchDto> SortUsers(IEnumerable<UserSearchDto> users, SortType? sort)
        {
            switch (sort)
            {
                default:
                case SortType.ByName:
                    users = users.OrderBy(u => u.UserName);
                    break;
                case SortType.ByNameDescending:
                    users = users.OrderByDescending(u => u.UserName);
                    break;
            }
            return users;
        }

        private IEnumerable<T> Pagination<T>(PaginationFilter filter, IEnumerable<T> data) where T : class
        {
            filter.Valid();
            if (filter.PageSize != 0)
                return data
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);
            return data;   
        }
    }
}
