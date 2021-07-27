﻿using AutoMapper;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces;
using Core.Interfaces.Search;
using Core.Response;
using Core.Services;
using Core.Services.Search;
using System.Collections.Generic;
using WebAPI.DTOs;

namespace Core.Repositories.Search
{
    class SearchRepository : ISearchRepository
    {
        private readonly ISearchAllRepository _allRepository;
        private readonly ISearchAuthorRepository _authorReposotory;
        private readonly ISearchUserRepository _userRepository;
        private readonly ISearchCategoryRepository _categoryReposotory;
        private readonly ISearchBookRepository _bookRepository;
        public SearchRepository(ISearchAllRepository allRepository, ISearchAuthorRepository authorRepository, ISearchUserRepository userRepository, ISearchCategoryRepository categoryReposotory, ISearchBookRepository bookRepository)
        {
            _allRepository = allRepository;
            _authorReposotory = authorRepository;
            _userRepository = userRepository;
            _categoryReposotory = categoryReposotory;
            _bookRepository = bookRepository;
        }

        public DataDto<SearchAll> SearchAll(PaginationFilter filter, string route, string searchString, SortType? sort)
        {
            return _allRepository.SearchAll(filter, route, searchString, sort);
        }

        public DataDto<AuthorDto> GetAuthors(PaginationFilter filter, string searchString, SortType? sort)
        {
            return _authorReposotory.GetAuthors(filter, searchString, sort);
        }

        public DataDto<CategoryDto> GetCategories(PaginationFilter filter, string searchString, SortType? sort)
        {
            return _categoryReposotory.GetCategories(filter, searchString, sort);
        }

        public DataDto<BookDto> GetBooks(PaginationFilter filter, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null)
        {
            return _bookRepository.GetBooks(filter, searchString, sort, minYear, maxYear, categoryId, authorId);
        }

        public DataDto<UserSearchDto> GetUsers(PaginationFilter filter, string searchString, SortType? sort)
        {
            return _userRepository.GetUsers(filter, searchString, sort);
        }
    }
}
