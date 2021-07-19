using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;
using Core.Interfaces;
using Core.Services;
using Core.ServiceResponses;
using System.Net;
using Core.Enums;

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
        public ServiceResponse All(SortType? sort, string searchString="")
        {
            return searchService.SearchAll(searchString, sort);
        }

        [HttpGet("Author")]
        public ServiceResponse Authors(SortType? sort, string searchString = "")
        {
            return searchService.SearchAuthor(searchString, sort);
        }

        [HttpGet("Books")]
        public ServiceResponse Books(SortType? sort, int? minYear, int? maxYear, int? categoryId, int? authorId, 
            string searchString = "")
        {
            return searchService.SearchBook(searchString, sort, minYear, maxYear, categoryId, authorId);
        }

        [HttpGet("Categories")]
        public ServiceResponse Categories(SortType? sort, string searchString ="")
        {
            return searchService.SearchCategory(searchString, sort);
        }

        [HttpGet("User")]
        public ServiceResponse Users(SortType? sort, string searchString = "")
        {
            return searchService.SearchUser(searchString, sort);
        }
    }
}
