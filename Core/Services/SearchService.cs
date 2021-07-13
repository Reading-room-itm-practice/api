using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Storage.Identity;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAPI.DTOs;
using System.Data.Entity;
using AutoMapper;

namespace Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly ICrudService<Author> _authors;
        private readonly ICrudService<Book> _books;
        private readonly ICrudService<Category> _categories;
        private readonly UserManager<User> _users;
        private readonly IMapper _mapper;

        public SearchService(ICrudService<Author> authors, ICrudService<Book> books, ICrudService<Category> categories, UserManager<User> users, IMapper mapper)
        {
            _authors = authors;
            _books = books;
            _categories = categories;
            _users = users;
            _mapper = mapper;
        }

        #region HelperMethods
        private bool ContainsQuerry(string name, string[] querries)
        {
            foreach (string querry in querries)
            {
                if (name.ToUpper().Contains(querry.ToUpper())) return true;
            }
            return false;
        }

        private string[] ProcessSearchString(string searchString)
        {
            searchString = Regex.Replace(searchString, @"\s+", " "); 
            return searchString.Split(" "); 
        }

        private IEnumerable<UserSearchDto> GetUsers(string[] searchQuerries)
        {
            var users = _users.Users.AsEnumerable().Where(u => ContainsQuerry(u.UserName, searchQuerries));
            return _mapper.Map<IEnumerable<UserSearchDto>>(users);
        }
        #endregion
        #region SortMethods
        public IEnumerable<AuthorResponseDto> SortAuthors(IEnumerable<AuthorResponseDto> authors, SortType? sort)
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
        public IEnumerable<CategoryResponseDto> SortCategories(IEnumerable<CategoryResponseDto> categories, SortType? sort)
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
        public IEnumerable<BookResponseDto> SortBooks(IEnumerable<BookResponseDto> books, SortType? sort)
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
        public IEnumerable<UserSearchDto> SortUsers(IEnumerable<UserSearchDto> users, SortType? sort)
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

        public Dictionary<string, IEnumerable<ISearchable>> SearchAll(string searchString, SortType? sort)
        {
            Dictionary<string, IEnumerable<ISearchable>> searchResults = new Dictionary<string, IEnumerable<ISearchable>>();

            var authors = SearchAuthor(searchString, sort);
            var books = SearchBook(searchString, sort);
            var categories = SearchCategory(searchString, sort);
            var users = SearchUser(searchString, sort);

            if (authors.Count() > 0)
                searchResults.Add(authors.First().GetType().ToString(), authors);
            if (books.Count() > 0)
                searchResults.Add(books.First().GetType().ToString(), books);
            if (categories.Count() > 0)
                searchResults.Add(categories.First().GetType().ToString(), categories);
            if (users.Count() > 0)
                searchResults.Add(users.First().GetType().ToString(), users);

            return searchResults;
        }

        public IEnumerable<CategoryResponseDto> SearchCategory(string searchString, SortType? sort)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var categories = _categories.GetAll<CategoryResponseDto>().Result.Where(c => ContainsQuerry(c.Name, searchQuerries));
            if (categories == null) return categories;

            categories = categories.Distinct();
            categories = SortCategories(categories, sort);

            return categories;
        }

        public IEnumerable<AuthorResponseDto> SearchAuthor(string searchString, SortType? sort)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var authors = _authors.GetAll<AuthorResponseDto>().Result.Where(a => ContainsQuerry(a.Name, searchQuerries));
            if (authors == null) return authors;

            authors = authors.Distinct();
            authors = SortAuthors(authors, sort);
            
            return authors;
        }

        public IEnumerable<BookResponseDto> SearchBook(string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var books = _books.GetAll<BookResponseDto>().Result.Where(b => ContainsQuerry(b.Title, searchQuerries));
            foreach (string querry in searchQuerries)
                if (int.TryParse(querry, out int year))
                {
                    var booksByYear = _books.GetAll<BookResponseDto>().Result.Where(b => b.ReleaseYear == year); //Books released in year "YYYY"
                    books = books.Concat(booksByYear);
                }

            if (categoryId != null) books = books.Where(b => b.CategoryId == categoryId);
            if (minYear != null) books = books.Where(b => b.ReleaseYear >= minYear);
            if (maxYear != null) books = books.Where(b => b.ReleaseYear <= maxYear);

            //books = books.Distinct<BookResponseDto>();
            var books1 = books.Distinct();
            books = SortBooks(books, sort);
            
            return books; 
        }

        public IEnumerable<BookResponseDto> SearchBook(string searchString, SortType? sort)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var books = _books.GetAll<BookResponseDto>().Result.Where(b => ContainsQuerry(b.Title, searchQuerries));
            if (books == null) return books;

            books = books.Distinct();
            books = SortBooks(books, sort);

            return books;
        }

        public IEnumerable<UserSearchDto> SearchUser(string searchString, SortType? sort)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var users = GetUsers(searchQuerries);
            if (users == null) return users;
            users = SortUsers(users, sort);

            return users;
        }
    }

    public enum SortType
    {
        byName,
        byNameDescending,
        byRelaseYear,
        byRelaseYearDescending
        //byId,
        //byAuthor,
        //byScore
    };
}
