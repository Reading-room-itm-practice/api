using Core.Enums;
using Core.Response;
using Core.Services;

namespace Core.Interfaces
{
    public interface ISearchService
    {
        public ServiceResponse SearchCategory(PaginationFilter filter, string searchString, SortType? sort);
        public ServiceResponse SearchBook(PaginationFilter filter, string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId,
            int? authorId);
        public ServiceResponse SearchAuthor(PaginationFilter filter, string searchString, SortType? sort);
        public ServiceResponse SearchUser(PaginationFilter filter, string searchString, SortType? sort);
        public ServiceResponse SearchAll(PaginationFilter filter, string searchString, SortType? sort);
    }
}
