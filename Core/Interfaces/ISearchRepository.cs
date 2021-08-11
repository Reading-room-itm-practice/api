using Core.DTOs;
using Core.Enums;
using Core.Services;
using Core.Services.Search;
using Storage.Identity;
using Storage.Interfaces;
using Storage.Models;

namespace Core.Interfaces
{
    public interface ISearchRepository
    {
        public ExtendedData GetEntities<T>(PaginationFilter filter, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null) where T : class;
    }
}
