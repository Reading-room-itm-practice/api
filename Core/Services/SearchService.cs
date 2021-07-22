﻿using Core.DTOs;
using Core.Enums;
using Core.Interfaces;
using Core.ServiceResponses;
using System.Collections.Generic;
using System.Linq;
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

        public ServiceResponse SearchAll(PaginationFilter filter, string searchString, SortType? sort)
        {
            Dictionary<SearchType, IEnumerable<object>> searchResults = new Dictionary<SearchType, IEnumerable<object>>();

            var authors = _searchRepository.GetAuthors(filter ,searchString, sort);
            var books = _searchRepository.GetBooks(filter, searchString, sort);
            var categories = _searchRepository.GetCategories(filter, searchString, sort);
            var users = _searchRepository.GetUsers(filter, searchString, sort);

            searchResults.Add(SearchType.Author, authors);
            searchResults.Add(SearchType.Book, books);
            searchResults.Add(SearchType.Category, categories);
            searchResults.Add(SearchType.User, users);

            foreach (KeyValuePair<SearchType, IEnumerable<object>> results in searchResults)
                if (results.Value.Count() != 0)
                    return new SuccessResponse<Dictionary<SearchType, IEnumerable<object>>>()
                    { Message = "Search results retrieved.", Content = searchResults };

            return new SuccessResponse<Dictionary<SearchType, IEnumerable<object>>>()
            { Message = "No search results found", Content = searchResults };
        }

        public ServiceResponse SearchAuthor(PaginationFilter filter, string searchString, SortType? sort)
        {
            var authors = _searchRepository.GetAuthors(filter, searchString, sort);

            if (authors.Count() == 0) return new SuccessResponse<IEnumerable<AuthorDto>>()
            { Message = "No author search results found.", Content = authors };

            return new SuccessResponse<IEnumerable<AuthorDto>>()
            { Message = "Author search results retrieved.", Content = authors };
        }

        public ServiceResponse SearchBook(PaginationFilter filter, string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId,
            int? authorId)
        {
            var books = _searchRepository.GetBooks(filter, searchString, sort, minYear, maxYear, categoryId, authorId);

            if (books.Count() == 0) return new SuccessResponse<IEnumerable<BookDto>>()
            { Message = "No book search results found.", Content = books };

            return new SuccessResponse<IEnumerable<BookDto>>()
            { Message = "Book search results retrieved.", Content = books };
        }

        public ServiceResponse SearchCategory(PaginationFilter filter, string searchString, SortType? sort)
        {
            var categories = _searchRepository.GetCategories(filter, searchString, sort);

            if (categories.Count() == 0) return new SuccessResponse<IEnumerable<CategoryDto>>()
            { Message = "No category search results found.", Content = categories };

            return new SuccessResponse<IEnumerable<CategoryDto>>()
            { Message = "Category search results retrieved.", Content = categories };
        }

        public ServiceResponse SearchUser(PaginationFilter filter, string searchString, SortType? sort)
        {
            var users = _searchRepository.GetUsers(filter, searchString, sort);

            if (users.Count() == 0) return new SuccessResponse<IEnumerable<UserSearchDto>>()
            { Message = "No user search results found.", Content = users };

            return new SuccessResponse<IEnumerable<UserSearchDto>>()
            { Message = "User search results retrieved.", Content = users };
        }
    }
}
