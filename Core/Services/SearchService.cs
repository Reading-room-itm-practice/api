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
using Core.ServiceResponses;
using System.Net;

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
            searchString = searchString ?? "@@@@";
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
        public IEnumerable<AuthorDto> SortAuthors(IEnumerable<AuthorDto> authors, SortType? sort)
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
        public IEnumerable<CategoryDto> SortCategories(IEnumerable<CategoryDto> categories, SortType? sort)
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
        public IEnumerable<BookDto> SortBooks(IEnumerable<BookDto> books, SortType? sort)
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
        #region GetMethods
        private IEnumerable<AuthorDto> GetAuthors(string searchString, SortType? sort)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var authors = _authors.GetAll<AuthorDto>().Result.Where(a => ContainsQuerry(a.Name, searchQuerries));
            if (authors.Count() == 0) return null;
            authors = authors.Distinct();
            authors = SortAuthors(authors, sort);
            return authors;
        }

        private IEnumerable<CategoryDto> GetCategories(string searchString, SortType? sort)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var categories = _categories.GetAll<CategoryDto>().Result.Where(c => ContainsQuerry(c.Name, searchQuerries));
            if (categories.Count() == 0) return null;
            categories = categories.Distinct();
            categories = SortCategories(categories, sort);
            return categories;
        }

        private IEnumerable<BookDto> GetBooks(string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var books = _books.GetAll<BookDto>().Result.Where(b => ContainsQuerry(b.Title, searchQuerries));
            foreach (string querry in searchQuerries)
                if (int.TryParse(querry, out int year))
                {
                    var booksByYear = _books.GetAll<BookDto>().Result.Where(b => b.ReleaseYear == year);
                    books = books.Concat(booksByYear);
                }

            if (categoryId != null) books = books.Where(b => b.CategoryId == categoryId);
            if (minYear != null) books = books.Where(b => b.ReleaseYear >= minYear);
            if (maxYear != null) books = books.Where(b => b.ReleaseYear <= maxYear);
            if (books.Count() == 0) return null;
            books = books.Distinct();
            books = SortBooks(books, sort);

            return books;
        }

        private IEnumerable<BookDto> GetBooks(string searchString, SortType? sort)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var books = _books.GetAll<BookDto>().Result.Where(b => ContainsQuerry(b.Title, searchQuerries));
            foreach (string querry in searchQuerries)
                if (int.TryParse(querry, out int year))
                {
                    var booksByYear = _books.GetAll<BookDto>().Result.Where(b => b.ReleaseYear == year);
                    books = books.Concat(booksByYear);
                }
            if (books.Count() == 0) return null;
            books = books.Distinct();
            books = SortBooks(books, sort);

            return books;
        }

        private IEnumerable<UserSearchDto> GetUsers(string searchString, SortType? sort)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var users = GetUsers(searchQuerries);
            if (users.Count() == 0) return null;
            users = users.Distinct();
            users = SortUsers(users, sort);
            return users;
        }
        #endregion
        #region ServiceResponseMethods
        public ServiceResponse SearchAll(string searchString, SortType? sort)
        {
            Dictionary<SearchType, IEnumerable<object>> searchResults = new Dictionary<SearchType, IEnumerable<object>>();
            //using ISearchable instead of object returns an empty json result eg:
            //          "content": {
            //              "Book": [
            //                { },
            //                { }
            //              ]
            //           },

            var authors = GetAuthors(searchString, sort);
            var books = GetBooks(searchString, sort);
            var categories = GetCategories(searchString, sort);
            var users = GetUsers(searchString, sort);

            if (authors != null)
                searchResults.Add(SearchType.Author, authors);
            if (books != null)
                searchResults.Add(SearchType.Book, books);
            if (categories != null)
                searchResults.Add(SearchType.Category, categories);
            if (users != null)
                searchResults.Add(SearchType.User, users);

            if (searchResults.Count == 0) return new SuccessResponse() 
                { Message = "No search results found", StatusCode = HttpStatusCode.OK };

            return new SuccessResponse<Dictionary<SearchType, IEnumerable<object>>>()
            { Message = "Search results retrieved.", StatusCode = HttpStatusCode.OK, Content = searchResults };
        }

        public ServiceResponse SearchAuthor(string searchString, SortType? sort)
        {
            var authors = GetAuthors(searchString, sort);

            if (authors == null) return new SuccessResponse()
                { Message = "No author search results found.", StatusCode = HttpStatusCode.OK };

            return new SuccessResponse<IEnumerable<AuthorDto>>()
            { Message = "Author search results retrieved.", StatusCode = HttpStatusCode.OK, Content = authors };
        }

        public ServiceResponse SearchBook(string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId)
        {
            var books = GetBooks(searchString, sort, minYear, maxYear, categoryId);

            if(books == null) return new SuccessResponse()
                { Message = "No book search results found.", StatusCode = HttpStatusCode.OK };

            return new SuccessResponse<IEnumerable<BookDto>>()
            { Message = "Book search results retrieved.", StatusCode = HttpStatusCode.OK, Content = books };
        }

        public ServiceResponse SearchBook(string searchString, SortType? sort)
        {
            var books = GetBooks(searchString, sort);
            if (books == null) return new SuccessResponse()
                { Message = "No book search results found.", StatusCode = HttpStatusCode.OK };

            return new SuccessResponse<IEnumerable<BookDto>>()
            { Message = "Book search results retrieved.", StatusCode = HttpStatusCode.OK, Content = books };
        }

        public ServiceResponse SearchCategory(string searchString, SortType? sort)
        {
            var categories = GetCategories(searchString, sort);

            if (categories == null) return new SuccessResponse()
                { Message = "No category search results found.", StatusCode = HttpStatusCode.OK };

            return new SuccessResponse<IEnumerable<CategoryDto>>()
            { Message = "Category search results retrieved.", StatusCode = HttpStatusCode.OK, Content = categories };
        }

        public ServiceResponse SearchUser(string searchString, SortType? sort)
        {
            var users = GetUsers(searchString, sort);
          
            if (users == null) return new SuccessResponse()
                { Message = "No user search results found.", StatusCode = HttpStatusCode.OK };

            return new SuccessResponse<IEnumerable<UserSearchDto>>()
            { Message = "User search results retrieved.", StatusCode = HttpStatusCode.OK, Content = users };
        }
        #endregion
    }

    public enum SortType
    {
        byName,
        byNameDescending,
        byRelaseYear,
        byRelaseYearDescending
    };

    public enum SearchType
    {
        Author,
        Book,
        Category,
        User
    };
}
