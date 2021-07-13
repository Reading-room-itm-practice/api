using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;
using Core.Interfaces;
using Core.Services;

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

        [HttpGet("{searchString}/")]
        public ActionResult All(string searchString, SortType? sort)
        {
            var searchResults = searchService.SearchAll(searchString, sort);
            return Ok(searchResults);
        }

        [HttpGet("Categories/{searchString}/")]
        public ActionResult Categories([Required]string searchString, SortType? sort)
        {
            var categories = searchService.SearchCategory(searchString, sort);
            return Ok(categories);
        }

        [HttpGet("Books/{searchString}/")]
        public ActionResult Books([Required] string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId)
        {
            var books = searchService.SearchBook(searchString, sort, minYear, maxYear, categoryId);
            return Ok(books);
        }

        [HttpGet("Author/{searchString}/")]
        public ActionResult Authors([Required] string searchString, SortType? sort)
        {
            var authors = searchService.SearchAuthor(searchString, sort);
            return Ok(authors);
        }

        [HttpGet("User/{searchString}/")]
        public ActionResult Users([Required] string searchString, SortType? sort)
        {
            var users = searchService.SearchUser(searchString, sort);
            return Ok(users);
        }
    }
}
