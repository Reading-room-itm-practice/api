using Core.DTOs;
using Core.Enums;
using Core.Services;
using System.Collections.Generic;
using WebAPI.DTOs;

namespace Core.Interfaces
{
    public interface ISearchRepository
    {
        public IEnumerable<AuthorDto> GetAuthors(PaginationFilter filter, string searchString, SortType? sort);
        public IEnumerable<CategoryDto> GetCategories(PaginationFilter filter, string searchString, SortType? sort);
        public IEnumerable<BookDto> GetBooks(PaginationFilter filter, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null);
        public IEnumerable<UserSearchDto> GetUsers(PaginationFilter filter, string searchString, SortType? sort);
    }
}
