using AutoMapper;
using Core.Common;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces;
using Core.Interfaces.Search;
using Core.Response;
using System.Collections.Generic;

namespace Core.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;
        public SearchService(ISearchRepository searchRepository, IUriService uriService, IMapper mapper)
        {
            _searchRepository = searchRepository;
            _uriService = uriService;
            _mapper = mapper;
        }

        public ServiceResponse SearchAll(PaginationFilter filter, string route, string searchString, SortType? sort)
        {
            var searchResults = _mapper.Map<DataDto<SearchAllDto>>(_searchRepository.SearchAll(filter, route, searchString, sort));
            var pagedReponse = PaginationHelper.CreatePagedReponse(searchResults.singleData, filter, searchResults.count, _uriService, route);
            var message = searchResults.count == 0 ? "No search results found" : "Search results retrieved.";

            return ServiceResponse<PagedResponse<SearchAllDto>>.Success(pagedReponse, message);
        }

        public ServiceResponse SearchAuthor(PaginationFilter filter, string route, string searchString, SortType? sort)
        {
            var authors = _mapper.Map<DataDto<AuthorDto>>(_searchRepository.GetAuthors(filter, searchString, sort));
            var pagedReponse = PaginationHelper.CreatePagedReponse(authors.data, filter, authors.count, _uriService, route);
            var message = authors.count == 0 ? "No author search results found." : "Author search results retrieved.";

            return ServiceResponse<PagedResponse<IEnumerable<AuthorDto>>>.Success(pagedReponse, message);
        }
        public ServiceResponse SearchBook(PaginationFilter filter, string route, string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId,
            int? authorId)
        {
            var books = _mapper.Map<DataDto<BookDto>>(_searchRepository.GetBooks(filter, searchString, sort, minYear, maxYear, categoryId, authorId));
            var pagedReponse = PaginationHelper.CreatePagedReponse(books.data, filter, books.count, _uriService, route);
            var message = books.count == 0 ? "No book search results found." : "Book search results retrieved.";

            return ServiceResponse<PagedResponse<IEnumerable<BookDto>>>.Success(pagedReponse, message);
        }

        public ServiceResponse SearchCategory(PaginationFilter filter, string route, string searchString, SortType? sort)
        {
            var categories = _mapper.Map<DataDto<CategoryDto>>(_searchRepository.GetCategories(filter, searchString, sort));
            var pagedReponse = PaginationHelper.CreatePagedReponse(categories.data, filter, categories.count, _uriService, route);
            var message = categories.count == 0 ? "No category search results found." : "Category search results retrieved.";

            return ServiceResponse<PagedResponse<IEnumerable<CategoryDto>>>.Success(pagedReponse, message);
        }

        public ServiceResponse SearchUser(PaginationFilter filter, string route, string searchString, SortType? sort)
        {
            var users = _mapper.Map<DataDto<UserSearchDto>>(_searchRepository.GetUsers(filter, searchString, sort));
            var pagedReponse = PaginationHelper.CreatePagedReponse(users.data, filter, users.count, _uriService, route);
            var message = users.count == 0 ? "No user search results found." : "User search results retrieved.";

            return ServiceResponse<PagedResponse<IEnumerable<UserSearchDto>>>.Success(pagedReponse, message);
        }
    }
}
