using Core.DTOs;
using Core.Enums;
using Core.Services;
using Core.Services.Search;
using WebAPI.DTOs;

namespace Core.Interfaces
{
    public interface ISearchRepository
    {
        public DataDto<SearchAll> SearchAll(PaginationFilter filter, string route, string searchString, SortType? sort);
        public DataDto<AuthorDto> GetAuthors(PaginationFilter filter, string searchString, SortType? sort);
        public DataDto<CategoryDto> GetCategories(PaginationFilter filter, string searchString, SortType? sort);
        public DataDto<BookDto> GetBooks(PaginationFilter filter, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null);
        public DataDto<UserSearchDto> GetUsers(PaginationFilter filter, string searchString, SortType? sort);
    }
}
