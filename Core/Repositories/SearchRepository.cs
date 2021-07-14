using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Storage.DataAccessLayer;
using Storage.Identity;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAPI.DTOs;

namespace Core.Repositories
{
    class SearchRepository : ISearchRepository
    {
        private readonly ApiDbContext _context;
        private readonly UserManager<User> _users;
        private readonly IMapper _mapper;
        public SearchRepository(ApiDbContext context, IMapper mapper, UserManager<User> users)
        {
            _context = context;
            _mapper = mapper;
            _users = users;
        }
        public IQueryable<AuthorDto> GetAuthors(string searchString, SortType? sort)
        {
            var searchQueries = ProcessSearchString(searchString);

            //it works
            var authors = (_mapper.Map<IEnumerable<AuthorDto>>(_context.Authors))
                .Where(a => ContainsQuery(a.Name, searchQueries)).AsQueryable();


            authors = authors.Distinct();
            authors = SortAuthors(authors, sort);
            return authors;
        }
        public IQueryable<CategoryDto> GetCategories(string searchString, SortType? sort)
        {
            var searchQueries = ProcessSearchString(searchString);
            var categories = _mapper.Map<IQueryable<CategoryDto>>(_context.Categories.Where(a => ContainsQuery(a.Name, searchQueries)));
            categories = categories.Distinct();
            categories = SortCategories(categories, sort);
            return categories;
        }
        public IQueryable<BookDto> GetBooks(string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId)
        {
            var searchQueries = ProcessSearchString(searchString);
            var books = _mapper.Map<IQueryable<BookDto>>(_context.Books.Where(a => ContainsQuery(a.Title, searchQueries)));

            foreach (string query in searchQueries)
                if (int.TryParse(query, out int year))
                {
                    var booksByYear = _mapper.Map<IQueryable<BookDto>>(_context.Books.Where(b => b.ReleaseYear == year));
                    books = books.Concat(booksByYear);
                }
            if (categoryId != null) books = books.Where(b => b.CategoryId == categoryId);
            if (minYear != null) books = books.Where(b => b.ReleaseYear >= minYear);
            if (maxYear != null) books = books.Where(b => b.ReleaseYear <= maxYear);
            books = books.Distinct();
            books = SortBooks(books, sort);

            return books;
        }

        public IQueryable<BookDto> GetBooks(string searchString, SortType? sort)
        {
            var searchQueries = ProcessSearchString(searchString);
            var books = _mapper.Map<IQueryable<BookDto>>(_context.Books.Where(a => ContainsQuery(a.Title, searchQueries)));

            foreach (string query in searchQueries)
                if (int.TryParse(query, out int year))
                {
                    var booksByYear = _mapper.Map<IQueryable<BookDto>>(_context.Books.Where(b => b.ReleaseYear == year));
                    books = books.Concat(booksByYear);
                }
            books = books.Distinct();
            books = SortBooks(books, sort);

            return books;
        }

        public IQueryable<UserSearchDto> GetUsers(string searchString, SortType? sort)
        {
            var searchQueries = ProcessSearchString(searchString);

            var users = GetUsers(searchQueries);
            users = users.Distinct();
            users = SortUsers(users, sort);
            return users;
        }

        #region HelperMethods
        private bool ContainsQuery(string name, string[] queries)
        {
            foreach (string query in queries)
            {
                if (name.ToUpper().Contains(query.ToUpper())) return true;
            }
            return false;
        }

        private string[] ProcessSearchString(string searchString)
        {
            searchString ??= string.Empty;
            while (searchString.StartsWith(' ')) searchString = searchString.Substring(1);
            searchString = Regex.Replace(searchString, @"\s+", " ");
            return searchString.Split(" ");
        }

        private IQueryable<UserSearchDto> GetUsers(string[] searchQueries)
        {
            var users = _users.Users.AsEnumerable().Where(u => ContainsQuery(u.UserName, searchQueries));
            return _mapper.Map<IQueryable<UserSearchDto>>(users);
        }
        #endregion
        #region SortMethods
        public IQueryable<AuthorDto> SortAuthors(IQueryable<AuthorDto> authors, SortType? sort)
        {
            switch (sort)
            {
                default:
                case SortType.byName:
                    authors = authors.OrderBy(a => a.Name);
                    break;
                case SortType.byNameDescending:
                    authors = authors.OrderByDescending(a => a.Name);
                    break;
            }
            return authors;
        }
        public IQueryable<CategoryDto> SortCategories(IQueryable<CategoryDto> categories, SortType? sort)
        {
            switch (sort)
            {
                default:
                case SortType.byName:
                    categories = categories.OrderBy(c => c.Name);
                    break;
                case SortType.byNameDescending:
                    categories = categories.OrderByDescending(c => c.Name);
                    break;
            }
            return categories;
        }
        public IQueryable<BookDto> SortBooks(IQueryable<BookDto> books, SortType? sort)
        {
            switch (sort)
            {
                default:
                case SortType.byName:
                    books = books.OrderBy(b => b.Title);
                    break;
                case SortType.byNameDescending:
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case SortType.byRelaseYear:
                    books = books.OrderBy(b => b.ReleaseYear);
                    break;
                case SortType.byRelaseYearDescending:
                    books = books.OrderByDescending(b => b.ReleaseYear);
                    break;
            }
            return books;
        }
        public IQueryable<UserSearchDto> SortUsers(IQueryable<UserSearchDto> users, SortType? sort)
        {
            switch (sort)
            {
                default:
                case SortType.byName:
                    users = users.OrderBy(u => u.UserName);
                    break;
                case SortType.byNameDescending:
                    users = users.OrderByDescending(u => u.UserName);
                    break;
            }
            return users;
        }
        #endregion
    }
}
