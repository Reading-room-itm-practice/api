using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Search;
using Core.Response;
using Core.Services;
using Core.Services.Search;
using Microsoft.AspNetCore.Mvc;
using Storage.Identity;
using Storage.Models;
using Swashbuckle.AspNetCore.Annotations;

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

        [SwaggerOperation(Summary = "Retrieves all authors, books, categories and users")]
        [HttpGet("All")]
        public ServiceResponse All([FromQuery] PaginationFilter filter, SortType? sort, string searchString = "")
        {
            var route = Request.Path.Value;
            return searchService.SearchEntity<AllData, SearchAllDto>(filter, route, searchString, sort);
        }

        [SwaggerOperation(Summary = "Retrieves all book authors")]
        [HttpGet("Authors")]
        public ServiceResponse Authors([FromQuery] PaginationFilter filter, SortType? sort, string searchString = "")
        {
            var route = Request.Path.Value;
            return searchService.SearchEntity<Author, AuthorDto>(filter, route, searchString, sort);
        }

        [SwaggerOperation(Summary = "Retrieves all books")]
        [HttpGet("Books")]
        public ServiceResponse Books([FromQuery] PaginationFilter filter, SortType? sort, int? minYear, int? maxYear, int? categoryId, int? authorId, string searchString = "")
        {
            var route = Request.Path.Value;
            return searchService.SearchEntity<Book, BookDto>(filter, route, searchString, sort, minYear, maxYear, categoryId, authorId);
        }

        [SwaggerOperation(Summary = "Retrieves all categories")]
        [HttpGet("Categories")]
        public ServiceResponse Categories([FromQuery] PaginationFilter filter, SortType? sort, string searchString = "")
        {
            var route = Request.Path.Value;
            return searchService.SearchEntity<Category, CategoryDto>(filter, route, searchString, sort);
        }

        [SwaggerOperation(Summary = "Retrieves all users")]
        [HttpGet("Users")]
        public ServiceResponse Users([FromQuery] PaginationFilter filter, SortType? sort, string searchString = "")
        {
            var route = Request.Path.Value;
            return searchService.SearchEntity<User, UserSearchDto>(filter, route, searchString, sort);
        }
    }
}
