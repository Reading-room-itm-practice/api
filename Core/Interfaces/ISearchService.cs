using Core.DTOs;
using Core.ServiceResponses;
using Core.Services;
using Storage.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;

namespace Core.Interfaces
{
    public interface ISearchService
    {
        public ServiceResponse SearchCategory(string searchString, SortType? sort);
        public ServiceResponse SearchBook(string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId);
        public ServiceResponse SearchBook(string searchString, SortType? sort);
        public ServiceResponse SearchAuthor(string searchString, SortType? sort);
        public ServiceResponse SearchUser(string searchString, SortType? sort);
        public ServiceResponse SearchAll(string searchString, SortType? sort);
    }
}
