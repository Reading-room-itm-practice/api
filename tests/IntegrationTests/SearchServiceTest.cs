using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Interfaces.Search;
using Core.Services;
using Core.Services.Search;
using Moq;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IntegrationTests
{
    public class SearchServiceTest
    {
        private ISearchService _searchService;
        public SearchServiceTest(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [Fact]
        public void SearchEntity_ReturnResponse()
        {

            var filter = new PaginationFilter(1, 4);
            var route = "/api/authors";

            var authorResponse = _searchService.SearchEntity<Author, AuthorDto>(filter, route, null, null);

            Assert.NotNull(authorResponse);
        }

    }
}