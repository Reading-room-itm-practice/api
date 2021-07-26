using Core.DTOs;
using Core.Enums;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface ISearchRepository
    {
        public IEnumerable<AuthorDto> GetAuthors(string searchString, SortType? sort);
        public IEnumerable<CategoryDto> GetCategories(string searchString, SortType? sort);
        public IEnumerable<BookDto> GetBooks(string searchString, SortType? sort, int? minYear = null, int? maxYear = null, 
            int? categoryId = null, int? authorId = null);
        public IEnumerable<UserSearchDto> GetUsers(string searchString, SortType? sort);
    }
}
