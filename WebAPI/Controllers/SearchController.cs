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
        public async Task<ActionResult> All(string searchString, SortType? sort)
        {
            var searchResults = await searchService.SearchAll(searchString, sort);
            return Ok(searchResults);
        }

        [HttpGet("Categories/{searchString}/")]
        public async Task<ActionResult> Categories([Required]string searchString, SortType? sort)
        {
            var categories = await searchService.SearchCategory(searchString, sort);
            return Ok(categories);
        }

        [HttpGet("Books/{searchString}/")]//{filter?}")]
        public async Task<ActionResult> Books([Required] string searchString, SortType? sort)
        {
            var books = await searchService.SearchBook(searchString, sort);
            return Ok(books);
        }

        [HttpGet("Author/{searchString}/")]
        public async Task<ActionResult> Authors([Required] string searchString, SortType? sort)
        {
            var authors = await searchService.SearchAuthor(searchString, sort);
            return Ok(authors);
        }

        [HttpGet("User/{searchString}/")]
        public async Task<ActionResult> Users([Required] string searchString, SortType? sort)
        {
            var users = await searchService.SearchUser(searchString, sort);
            return Ok(users);
        }
    }
}
