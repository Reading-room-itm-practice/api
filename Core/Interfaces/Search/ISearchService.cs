using Core.Enums;
using Core.Response;
using Core.Services;
using Storage.Interfaces;

namespace Core.Interfaces.Search
{
    public interface ISearchService
    {
        public ServiceResponse SearchCategory(PaginationFilter filter, string route, string searchString, SortType? sort);
        public ServiceResponse SearchBook(PaginationFilter filter, string route, string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId,
            int? authorId);
        public ServiceResponse SearchAuthor(PaginationFilter filter, string route, string searchString, SortType? sort);
        public ServiceResponse SearchUser(PaginationFilter filter, string route, string searchString, SortType? sort);
        public ServiceResponse SearchAll(PaginationFilter filter, string route, string searchString, SortType? sort);
        public ServiceResponse SearchEntity<T>(PaginationFilter filter, string route, string searchString, SortType? sort) where T : class;
    }
}
