using Core.Enums;
using Core.ServiceResponses;

namespace Core.Interfaces
{
    public interface ISearchService
    {
        public ServiceResponse SearchCategory(string searchString, SortType? sort);
        public ServiceResponse SearchBook(string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId, 
            int? authorId);
        public ServiceResponse SearchAuthor(string searchString, SortType? sort);
        public ServiceResponse SearchUser(string searchString, SortType? sort);
        public ServiceResponse SearchAll(string searchString, SortType? sort);
    }
}
