using Core.DTOs;
using Core.Enums;
using Core.Services;
using Storage.Models;
using System.Collections.Generic;

namespace Core.Interfaces.Search
{
    interface ISearchBookRepository
    {
        DataDto<IEnumerable<Book>> GetBooks(PaginationFilter filter, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null);
    }
}
