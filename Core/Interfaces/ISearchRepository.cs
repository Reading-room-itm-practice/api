using Core.DTOs;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DTOs;

namespace Core.Interfaces
{
    public interface ISearchRepository
    {
        public IQueryable<AuthorDto> GetAuthors(string searchString, SortType? sort);
        public IQueryable<CategoryDto> GetCategories(string searchString, SortType? sort);
        public IQueryable<BookDto> GetBooks(string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId);
        public IQueryable<BookDto> GetBooks(string searchString, SortType? sort);
        public IQueryable<UserSearchDto> GetUsers(string searchString, SortType? sort);
    }
}
