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
        //Task<>, make async
        public Task<IEnumerable<CategoryResponseDto>> SearchCategory(string searchString, SortType? sort);
        public Task<IEnumerable<BookResponseDto>> SearchBook(string searchString, SortType? sort);
        public Task<IEnumerable<AuthorResponseDto>> SearchAuthor(string searchString, SortType? sort);
        public Task<IEnumerable<UserSearchDto>> SearchUser(string searchString, SortType? sort);
        public Task<Dictionary<string, IEnumerable<object>>> SearchAll(string searchString, SortType? sort);
    }
}
