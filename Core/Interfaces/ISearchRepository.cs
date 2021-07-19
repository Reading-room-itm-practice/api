﻿using Core.DTOs;
using Core.Enums;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DTOs;

namespace Core.Interfaces
{
    public interface ISearchRepository
    {
        public IEnumerable<AuthorDto> GetAuthors(string searchString, SortType? sort);
        public IEnumerable<CategoryDto> GetCategories(string searchString, SortType? sort);
        public IEnumerable<BookDto> GetBooks(string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId, int? authorId);
        public IEnumerable<BookDto> GetBooks(string searchString, SortType? sort);
        public IEnumerable<UserSearchDto> GetUsers(string searchString, SortType? sort);
    }
}
