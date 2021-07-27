using Core.Enums;
using Core.Interfaces.Search;
using Core.Response;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

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
            return searchService.SearchAll(filter, route, searchString, sort);
        }

        [HttpGet("Author")]
        public ServiceResponse Authors([FromQuery] PaginationFilter filter, SortType? sort, string searchString = "")
        {
            var route = Request.Path.Value;
            return searchService.SearchAuthor(filter, route, searchString, sort);
        }

        [HttpGet("Books")]
        public ServiceResponse Books([FromQuery] PaginationFilter filter, SortType? sort, int? minYear, int? maxYear, int? categoryId, int? authorId,
            string searchString = "")
        {
            var route = Request.Path.Value;
            return searchService.SearchBook(filter, route, searchString, sort, minYear, maxYear, categoryId, authorId);
        }

        [HttpGet("Categories")]
        public ServiceResponse Categories([FromQuery] PaginationFilter filter, SortType? sort, string searchString = "")
        {
            var route = Request.Path.Value;
            return searchService.SearchCategory(filter, route, searchString, sort);
        }

        [HttpGet("User")]
        public ServiceResponse Users([FromQuery] PaginationFilter filter, SortType? sort, string searchString = "")
        {
            var route = Request.Path.Value;
            return searchService.SearchUser(filter, route, searchString, sort);
        }
    }
}
