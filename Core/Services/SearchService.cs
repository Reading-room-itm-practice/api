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

        private bool ContainsQuerryBook(BookResponseDto book, string[] querries)
        {
            foreach (string querry in querries)
            {
                if (book.Title.ToUpper().Contains(querry.ToUpper())) return true;
                else if (_authors.GetById<Author>(book.AuthorId).Result.Name.ToUpper().Contains(querry.ToUpper())) return true;
            }
            return false;
        }

        private string[] ProcessSearchString(string searchString)
        {
            searchString = Regex.Replace(searchString, @"\s+", " "); 
            return searchString.Split(" "); 
        }

        private async Task<IEnumerable<UserSearchDto>> GetUsers(string[] searchQuerries)
        {
            var users = await Task.Run(() => _users.Users.AsEnumerable().Where(u => ContainsQuerry(u.UserName, searchQuerries)));
            return _mapper.Map<IEnumerable<UserSearchDto>>(users);
        }
        #endregion

        public async Task<Dictionary<string, IEnumerable<object>>> SearchAll(string searchString, SortType? sort)
        {
            Dictionary<string, IEnumerable<object>> searchResults = new Dictionary<string, IEnumerable<object>>();

            var categories = await SearchCategory(searchString, sort);
            var books = await SearchBook(searchString, sort);
            var authors = await SearchAuthor(searchString, sort);
            var users = await SearchUser(searchString, sort);

            if (categories.Count() > 0)
                searchResults.Add(categories.First().GetType().ToString(), categories);
            if (books.Count() > 0)
                searchResults.Add(books.First().GetType().ToString(), books);
            if (authors.Count() > 0)
                searchResults.Add(authors.First().GetType().ToString(), authors);
            if (users.Count() > 0)
                searchResults.Add(users.First().GetType().ToString(), users);
            return searchResults;
        }

        public async Task<IEnumerable<CategoryResponseDto>> SearchCategory(string searchString, SortType? sort)
        {
            
            var searchQuerries = ProcessSearchString(searchString);
            var categories = await Task.Run(() => _categories.GetAll<CategoryResponseDto>().Result.Where(c => ContainsQuerry(c.Name, searchQuerries)));
                
            switch (sort)
            {
                default:
                case SortType.byName:
                    categories = categories.OrderBy(c => c.Name);
                    break;
            }

            return categories;
        }

        public async Task<IEnumerable<AuthorResponseDto>> SearchAuthor(string searchString, SortType? sort)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var authors = await Task.Run(() => _authors.GetAll<AuthorResponseDto>().Result.Where(a => ContainsQuerry(a.Name, searchQuerries)));

            switch (sort)
            {
                default:
                case SortType.byName:
                    authors = authors.OrderBy(a => a.Name);
                    break;
            }
            return authors;
        }

        public async Task<IEnumerable<BookResponseDto>> SearchBook(string searchString, SortType? sort)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var books = await Task.Run(() => _books.GetAll<BookResponseDto>().Result.Where(b => ContainsQuerry(b.Title, searchQuerries)));

            switch (sort)
            {
                default:
                case SortType.byName:
                    books = books.OrderBy(b => b.Title);
                    break;
            }
            return books; 
        }

        public async Task<IEnumerable<UserSearchDto>> SearchUser(string searchString, SortType? sort)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var users = await GetUsers(searchQuerries);

            switch (sort)
            {
                default:
                case SortType.byName:
                    users = users.OrderBy(u => u.UserName);
                    break;
            }
            return users;
        }
    }

    public enum SortType
    {
        byName,
        byId,
        byAuthor,
        byScore,
    };
}
