using Core.DTOs;
using Core.Enums;
using Core.Interfaces;
using Core.Interfaces.Search;
using Core.Services;
using Core.Services.Search;
using Storage.Models;

namespace Core.Repositories.Search
{
    class SearchRepository : ISearchRepository
    {
        private readonly ISearchAllRepository _allRepository;
        private readonly ISearchBookRepository _bookRepository;
        private readonly IGenericSearchRepository _genericRepository;
        public SearchRepository(ISearchAllRepository allRepository, ISearchBookRepository bookRepository, IGenericSearchRepository genericRepository)
        {
            _allRepository = allRepository;
            _bookRepository = bookRepository;
            _genericRepository = genericRepository;
        }

        public ExtendedData GetEntities<T>(PaginationFilter filter, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null) where T : class
        {
            if (typeof(T) == typeof(Book))
                return _bookRepository.GetBooks(filter, searchString, sort, minYear, maxYear, categoryId, authorId);
            if (typeof(T) == typeof(AllData))
                return _allRepository.SearchAll(filter, searchString, sort);
            
            return _genericRepository.GetEntities<T>(filter, searchString, sort);
        }
    }
}
