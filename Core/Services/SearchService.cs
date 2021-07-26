using Core.DTOs;
using Core.Enums;
using Core.Interfaces;
using Core.ServiceResponses;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public ServiceResponse SearchAll(string searchString, SortType? sort)
        {
            Dictionary<SearchType, IEnumerable<object>> searchResults = new Dictionary<SearchType, IEnumerable<object>>();

            var authors = _searchRepository.GetAuthors(searchString, sort);
            var books = _searchRepository.GetBooks(searchString, sort);
            var categories = _searchRepository.GetCategories(searchString, sort);
            var users = _searchRepository.GetUsers(searchString, sort);

            searchResults.Add(SearchType.Author, authors);
            searchResults.Add(SearchType.Book, books);
            searchResults.Add(SearchType.Category, categories);
            searchResults.Add(SearchType.User, users);

            foreach (KeyValuePair<SearchType, IEnumerable<object>> results in searchResults)
            {
                if (results.Value.Count() != 0)
                {
                    return ServiceResponse<Dictionary<SearchType, IEnumerable<object>>>.Success(searchResults, "Search results retrieved.");
                }
            }

            return ServiceResponse<Dictionary<SearchType, IEnumerable<object>>>.Success(searchResults, "No search results found");
        }

        public ServiceResponse SearchAuthor(string searchString, SortType? sort)
        {
            var authors = _searchRepository.GetAuthors(searchString, sort);
            var message = authors.Count() == 0 ? "No author search results found." : "Author search results retrieved.";

            return ServiceResponse<IEnumerable<AuthorDto>>.Success(authors, message);
        }

        public ServiceResponse SearchBook(string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId,
            int? authorId)
        {
            var books = _searchRepository.GetBooks(searchString, sort, minYear, maxYear, categoryId, authorId);
            var message = books.Count() == 0 ? "No book search results found." : "Book search results retrieved.";

            return ServiceResponse<IEnumerable<BookDto>>.Success(books, message);
        }

        public ServiceResponse SearchCategory(string searchString, SortType? sort)
        {
            var categories = _searchRepository.GetCategories(searchString, sort);
            var message = categories.Count() == 0 ? "No category search results found." : "Category search results retrieved.";

            return ServiceResponse<IEnumerable<CategoryDto>>.Success(categories, message);
        }

        public ServiceResponse SearchUser(string searchString, SortType? sort)
        {
            var users = _searchRepository.GetUsers(searchString, sort);
            var message = users.Count() == 0 ? "No user search results found." : "User search results retrieved.";

            return ServiceResponse<IEnumerable<UserSearchDto>>.Success(users, message);
        }
    }
}
