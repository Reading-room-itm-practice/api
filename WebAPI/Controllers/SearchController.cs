using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Services;
using WebAPI.DTOs;
using WebAPI.DataAccessLayer;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;

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

        [HttpGet("{searchString}/{sort?}")]
        public ActionResult All(string searchString, SortType sort)
        {
            Dictionary<string, IEnumerable<object>> searchResults = new Dictionary<string, IEnumerable<object>>();

            var categories = searchService.SearchCategory(searchString, sort);
            var books = searchService.SearchBook(searchString, sort);
            var authors = searchService.SearchAuthor(searchString, sort);
            //...

            if (categories.Count() > 0)
                searchResults.Add(categories.First().GetType().ToString(), categories);
            if (books.Count() > 0)
                searchResults.Add(books.First().GetType().ToString(), books);
            if (authors.Count() > 0)
                searchResults.Add(authors.First().GetType().ToString(), authors);
            //...
            return Ok(searchResults);
        }

        [HttpGet("Categories/{searchString}/{sort?}")]
        public ActionResult Categories([Required]string searchString, SortType? sort)
        {
            var categories = searchService.SearchCategory(searchString, sort);
            return Ok(categories);
        }

        [HttpGet("Books/{searchString}/{sort?}")]//{filter?}")]
        public ActionResult Books([Required] string searchString, SortType? sort)
        {
            var books = searchService.SearchBook(searchString, sort);
            return Ok(books);
        }

        [HttpGet("Author/{searchString}/{sort?}")]
        public ActionResult Authors([Required] string searchString, SortType? sort)
        {
            var authors = searchService.SearchAuthor(searchString, sort);
            return Ok(authors);
        }

        [HttpGet("User/{searchString}/{sort?}")]
        public ActionResult Users([Required] string searchString, SortType? sort)
        {
            //var users = searchService.SearchUser(searchString, sort);
            //return Ok(users);
            return Ok();
        }
    }
}
