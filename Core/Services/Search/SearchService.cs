using Core.Common;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces;
using Core.Interfaces.Search;
using Core.Response;
using System.Collections.Generic;
using System.Linq;
using WebAPI.DTOs;

namespace Core.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IUriService _uriService;
        public SearchService(ISearchRepository searchRepository, IUriService uriService)
        {
            _searchRepository = searchRepository;
            _uriService = uriService;
        }

        public ServiceResponse SearchAll(PaginationFilter filter, string route, string searchString, SortType? sort)
        {
            var searchResults = _searchRepository.SearchAll(filter, route, searchString, sort);
            var pagedReponse = PaginationHelper.CreatePagedReponse(searchResults.singleData, filter, searchResults.count, _uriService, route);

            if (searchResults.singleData.Count() != 0)
                return new SuccessResponse<PagedResponse<SearchAll>>()
                { Message = "Search results retrieved.", Content = pagedReponse };

            return new SuccessResponse<PagedResponse<SearchAll>>()
            { Message = "No search results found", Content = pagedReponse };
        }

        public ServiceResponse SearchAuthor(PaginationFilter filter, string route, string searchString, SortType? sort)
        {
            var authors = _searchRepository.GetAuthors(filter, searchString, sort);
            var pagedReponse = PaginationHelper.CreatePagedReponse(authors.data, filter, authors.count, _uriService, route);

            if (authors.count == 0) return new SuccessResponse<PagedResponse<IEnumerable<AuthorDto>>>()
            { Message = "No author search results found.", Content = pagedReponse };

            return new SuccessResponse<PagedResponse<IEnumerable<AuthorDto>>>()
            { Message = "Author search results retrieved.", Content = pagedReponse };
        }

        public ServiceResponse SearchBook(PaginationFilter filter, string route, string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId,
            int? authorId)
        {
            var books = _searchRepository.GetBooks(filter, searchString, sort, minYear, maxYear, categoryId, authorId);
            var pagedReponse = PaginationHelper.CreatePagedReponse(books.data, filter, books.count, _uriService, route);

            if (books.count == 0) return new SuccessResponse<PagedResponse<IEnumerable<BookDto>>>()
            { Message = "No book search results found.", Content = pagedReponse };

            return new SuccessResponse<PagedResponse<IEnumerable<BookDto>>>()
            { Message = "Book search results retrieved.", Content = pagedReponse };
        }

        public ServiceResponse SearchCategory(PaginationFilter filter, string route, string searchString, SortType? sort)
        {
            var categories = _searchRepository.GetCategories(filter, searchString, sort);
            var pagedReponse = PaginationHelper.CreatePagedReponse(categories.data, filter, categories.count, _uriService, route);

            if (categories.count == 0) return new SuccessResponse<PagedResponse<IEnumerable<CategoryDto>>>()
            { Message = "No category search results found.", Content = pagedReponse };

            return new SuccessResponse<PagedResponse<IEnumerable<CategoryDto>>>()
            { Message = "Category search results retrieved.", Content = pagedReponse };
        }

        public ServiceResponse SearchUser(PaginationFilter filter, string route, string searchString, SortType? sort)
        {
            var users = _searchRepository.GetUsers(filter, searchString, sort);
            var pagedReponse = PaginationHelper.CreatePagedReponse(users.data, filter, users.count, _uriService, route);

            if (users.count == 0) return new SuccessResponse<PagedResponse<IEnumerable<UserSearchDto>>>()
            { Message = "No user search results found.", Content = pagedReponse };

            return new SuccessResponse<PagedResponse<IEnumerable<UserSearchDto>>>()
            { Message = "User search results retrieved.", Content = pagedReponse };
        }
    }
}
