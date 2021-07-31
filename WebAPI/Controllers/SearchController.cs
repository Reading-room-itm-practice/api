using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Search;
using Core.Response;
using Core.Services;
using Core.Services.Search;
using Microsoft.AspNetCore.Mvc;
using Storage.Identity;
using Storage.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet]
        public ServiceResponse All([FromQuery] PaginationFilter filter, SortType? sort, string searchString = "")
        {
            var route = Request.Path.Value;
            return searchService.SearchEntity<SearchAll, SearchAllDto>(filter, route, searchString, sort);
        }

        [HttpGet("Author")]
        public ServiceResponse Authors([FromQuery] PaginationFilter filter, SortType? sort, string searchString = "")
        {
            var route = Request.Path.Value;
            return searchService.SearchEntity<Author, AuthorDto>(filter, route, searchString, sort);
        }

        [HttpGet("Books")]
        public ServiceResponse Books([FromQuery] PaginationFilter filter, SortType? sort, int? minYear, int? maxYear, int? categoryId, int? authorId,
            string searchString = "")
        {
            var route = Request.Path.Value;
            return searchService.SearchEntity<Book, BookDto>(filter, route, searchString, sort, minYear, maxYear, categoryId, authorId);
        }

        [HttpGet("Categories")]
        public ServiceResponse Categories([FromQuery] PaginationFilter filter, SortType? sort, string searchString = "")
        {
            var route = Request.Path.Value;
            return searchService.SearchEntity<Category, CategoryDto>(filter, route, searchString, sort);
        }

        [HttpGet("User")]
        public ServiceResponse Users([FromQuery] PaginationFilter filter, SortType? sort, string searchString = "")
        {
            var route = Request.Path.Value;
            return searchService.SearchEntity<User, UserSearchDto>(filter, route, searchString, sort);
        }
    }
}
