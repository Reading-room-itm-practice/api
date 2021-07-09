using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Identity;

namespace WebAPI.Services
{
    public interface ISearchService
    {
        //Task<>, make async
        public IEnumerable<CategoryDTO> SearchCategory(string searchString, SortType? sort);
        public IEnumerable<BookResponseDto> SearchBook(string searchString, SortType? sort);
        public IEnumerable<AuthorResponseDto> SearchAuthor(string searchString, SortType? sort);
        public Task<IQueryable<User>> SearchUser(string searchString, SortType? sort);
    }
}
