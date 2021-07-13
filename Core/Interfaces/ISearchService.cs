using Core.DTOs;
using Core.Services;
using Storage.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Identity;

namespace Core.Interfaces
{
    public interface ISearchService
    {
        public IEnumerable<CategoryResponseDto> SearchCategory(string searchString, SortType? sort);
        public IEnumerable<BookResponseDto> SearchBook(string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId);
        public IEnumerable<AuthorResponseDto> SearchAuthor(string searchString, SortType? sort);
        public IEnumerable<UserSearchDto> SearchUser(string searchString, SortType? sort);
        public Dictionary<string, IEnumerable<ISearchable>> SearchAll(string searchString, SortType? sort);
    }
}
