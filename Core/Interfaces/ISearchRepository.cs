using Core.DTOs;
using Core.Enums;
using Core.Services;
using Core.Services.Search;
using Storage.Identity;
using Storage.Models;

namespace Core.Interfaces
{
    public interface ISearchRepository
    {
        public DataDto<SearchAll> SearchAll(PaginationFilter filter, string route, string searchString, SortType? sort);
        public DataDto<Author> GetAuthors(PaginationFilter filter, string searchString, SortType? sort);
        public DataDto<Category> GetCategories(PaginationFilter filter, string searchString, SortType? sort);
        public DataDto<Book> GetBooks(PaginationFilter filter, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null);
        public DataDto<User> GetUsers(PaginationFilter filter, string searchString, SortType? sort);
    }
}
