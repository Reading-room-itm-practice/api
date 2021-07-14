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
using System.Data.Entity;
using AutoMapper;
using Core.ServiceResponses;
using System.Net;
using WebAPI.DTOs;

namespace Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public ServiceResponse SearchAll(string searchString, SortType? sort)
        {
            Dictionary<SearchType, IQueryable<object>> searchResults = new Dictionary<SearchType, IQueryable<object>>();

            var authors = _searchRepository.GetAuthors(searchString, sort);
            var books = _searchRepository.GetBooks(searchString, sort);
            var categories = _searchRepository.GetCategories(searchString, sort);
            var users = _searchRepository.GetUsers(searchString, sort);

            searchResults.Add(SearchType.Author, authors);
            searchResults.Add(SearchType.Book, books);
            searchResults.Add(SearchType.Category, categories);
            searchResults.Add(SearchType.User, users);

            if (searchResults.Count == 0) return new SuccessResponse() 
                { Message = "No search results found", StatusCode = HttpStatusCode.OK };

            return new SuccessResponse<Dictionary<SearchType, IQueryable<object>>>()
            { Message = "Search results retrieved.", StatusCode = HttpStatusCode.OK, Content = searchResults };
        }

        public ServiceResponse SearchAuthor(string searchString, SortType? sort)
        {
            var authors = _searchRepository.GetAuthors(searchString, sort);

            if (authors == null) return new SuccessResponse()
                { Message = "No author search results found.", StatusCode = HttpStatusCode.OK };

            return new SuccessResponse<IQueryable<AuthorDto>>()
            { Message = "Author search results retrieved.", StatusCode = HttpStatusCode.OK, Content = authors };
        }

        public ServiceResponse SearchBook(string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId)
        {
            var books = _searchRepository.GetBooks(searchString, sort, minYear, maxYear, categoryId);

            if(books == null) return new SuccessResponse()
                { Message = "No book search results found.", StatusCode = HttpStatusCode.OK };

            return new SuccessResponse<IQueryable<BookDto>>()
            { Message = "Book search results retrieved.", StatusCode = HttpStatusCode.OK, Content = books };
        }

        public ServiceResponse SearchBook(string searchString, SortType? sort)
        {
            var books = _searchRepository.GetBooks(searchString, sort);
            if (books == null) return new SuccessResponse()
                { Message = "No book search results found.", StatusCode = HttpStatusCode.OK };

            return new SuccessResponse<IQueryable<BookDto>>()
            { Message = "Book search results retrieved.", StatusCode = HttpStatusCode.OK, Content = books };
        }

        public ServiceResponse SearchCategory(string searchString, SortType? sort)
        {
            var categories = _searchRepository.GetCategories(searchString, sort);

            if (categories == null) return new SuccessResponse()
                { Message = "No category search results found.", StatusCode = HttpStatusCode.OK };

            return new SuccessResponse<IQueryable<CategoryDto>>()
            { Message = "Category search results retrieved.", StatusCode = HttpStatusCode.OK, Content = categories };
        }

        public ServiceResponse SearchUser(string searchString, SortType? sort)
        {
            var users = _searchRepository.GetUsers(searchString, sort);
          
            if (users == null) return new SuccessResponse()
                { Message = "No user search results found.", StatusCode = HttpStatusCode.OK };

            return new SuccessResponse<IQueryable<UserSearchDto>>()
            { Message = "User search results retrieved.", StatusCode = HttpStatusCode.OK, Content = users };
        }
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
