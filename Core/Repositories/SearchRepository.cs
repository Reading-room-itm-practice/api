using AutoMapper;
using Core.DTOs;
using Core.Enums;
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
        private readonly IMapper _mapper;
        public SearchRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<AuthorDto> GetAuthors(string searchString, SortType? sort)
        {
            var searchQueries = ProcessSearchString(searchString);
            var authors = (_mapper.Map<IEnumerable<AuthorDto>>(_context.Authors))
                .Where(a => ContainsQuery(a.Name, searchQueries));

            authors = authors.Distinct();
            authors = SortAuthors(authors, sort);
            return authors;
        }

        public IEnumerable<CategoryDto> GetCategories(string searchString, SortType? sort)
        {
            var searchQueries = ProcessSearchString(searchString);
            var categories = (_mapper.Map<IEnumerable<CategoryDto>>(_context.Categories))
                .Where(c => ContainsQuery(c.Name, searchQueries));

            categories = categories.Distinct();
            categories = SortCategories(categories, sort);
            return categories;
        }

        public IEnumerable<BookDto> GetBooks(string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId)
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
            if (minYear != null) books = books.Where(b => b.ReleaseYear >= minYear);
            if (maxYear != null) books = books.Where(b => b.ReleaseYear <= maxYear);
            books = books.Distinct();
            books = SortBooks(books, sort);
            return books;
        }

        public IEnumerable<BookDto> GetBooks(string searchString, SortType? sort)
        {
            var searchQueries = ProcessSearchString(searchString);
            var books = (_mapper.Map<IEnumerable<BookDto>>(_context.Books))
                            .Where(b => ContainsQuery(b.Title, searchQueries));

            foreach (string query in searchQueries)
                if (int.TryParse(query, out int year) && year.ToString().Length == 4)
                {
                    var booksByYear = (_mapper.Map<IEnumerable<BookDto>>(_context.Books))
                            .Where(b => b.ReleaseYear == year);
                    books = books.Concat(booksByYear);
                }

            books = books.Distinct();
            books = SortBooks(books, sort);
            return books;
        }

        public IEnumerable<UserSearchDto> GetUsers(string searchString, SortType? sort)
        {
            var searchQueries = ProcessSearchString(searchString);
            var users = GetUsers(searchQueries);
            users = users.Distinct();
            users = SortUsers(users, sort);
            return users;
        }

        private IEnumerable<UserSearchDto> GetUsers(string[] searchQueries)
        {
            var users = (_mapper.Map<IEnumerable<UserSearchDto>>(_context.Users))
                            .Where(u => ContainsQuery(u.UserName, searchQueries));
            return users;
        }

        private bool ContainsQuery(string name, string[] queries)
        {
            foreach (string query in queries)
            {
                //if (name.ToUpper().Contains(query.ToUpper())) return true; //only one query must match the search name
                if (queries.All(q => name.ToUpper().Contains(q.ToUpper()))) return true; //all queries must match the search name
            }
            return false;
        }

        private string[] ProcessSearchString(string searchString)
        {
            searchString = Regex.Replace(searchString, @"\s+", " ");
            return searchString.Split(" ");
        }

        private IEnumerable<AuthorDto> SortAuthors(IEnumerable<AuthorDto> authors, SortType? sort)
        {
            switch (sort)
            {
                default:
                case SortType.ByName:
                    authors = authors.OrderBy(a => a.Name);
                    break;
                case SortType.ByNameDescending:
                    authors = authors.OrderByDescending(a => a.Name);
                    break;
            }
            return authors;
        }

        private IEnumerable<CategoryDto> SortCategories(IEnumerable<CategoryDto> categories, SortType? sort)
        {
            switch (sort)
            {
                default:
                case SortType.ByName:
                    categories = categories.OrderBy(c => c.Name);
                    break;
                case SortType.ByNameDescending:
                    categories = categories.OrderByDescending(c => c.Name);
                    break;
            }
            return categories;
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
    }
}
