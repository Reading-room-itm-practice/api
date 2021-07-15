﻿using Microsoft.AspNetCore.Mvc;
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
        public ServiceResponse All([Required] string searchString, SortType? sort)
        {
            return searchService.SearchAll(searchString, sort);
        }

        [HttpGet("Author/{searchString}/")]
        public ServiceResponse Authors([Required] string searchString, SortType? sort)
        {
            return searchService.SearchAuthor(searchString, sort);
        }

        [HttpGet("Books/{searchString}/")]
        public ServiceResponse Books([Required] string searchString, SortType? sort, int? minYear, int? maxYear, int? categoryId)
        {
            if (minYear == null && maxYear == null && categoryId == null) return searchService.SearchBook(searchString, sort);
            return searchService.SearchBook(searchString, sort, minYear, maxYear, categoryId);
        }

        [HttpGet("Categories/{searchString}/")]
        public ServiceResponse Categories([Required] string searchString, SortType? sort)
        {
            return searchService.SearchCategory(searchString, sort);
        }

        [HttpGet("User/{searchString}/")]
        public ServiceResponse Users([Required] string searchString, SortType? sort)
        {
            return searchService.SearchUser(searchString, sort);
        }
    }
}
