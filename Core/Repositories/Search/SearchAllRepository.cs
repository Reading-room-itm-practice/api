using AutoMapper;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Search;
using Core.Services;
using Core.Services.Search;
using Storage.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Repositories.Search
{
    class SearchAllRepository : ISearchAllRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        private IQueryable _authors, _categories, _books, _users;
        public SearchAllRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            _authors = _context.Authors;
            _categories = _context.Categories;
            _books = _context.Books;
            _users = _context.Users;
        }

        public DataDto<SearchAll> SearchAll(PaginationFilter filter, string route, string searchString, SortType? sort)
        {
            var searchQueries = AdditionalSearchMethods.ProcessSearchString(searchString);

            SortQueries(sort);

            var entities = new SearchAll();
            var toSkip = (filter.PageNumber - 1) * filter.PageSize;
            var toTake = filter.PageSize;

            if (filter.PageSize != 0)
            {
                entities.Authors = (_mapper.Map<IEnumerable<AuthorDto>>(_authors))
                    .Where(a => AdditionalSearchMethods.ContainsQuery(a.Name, searchQueries)).Skip(toSkip).Take(toTake);
                toSkip = Math.Clamp(toSkip - _context.Authors.Count(), 0, toSkip);
                toTake -= entities.Authors.Count();

                entities.Categories = (_mapper.Map<IEnumerable<CategoryDto>>(_categories))
                    .Where(c => AdditionalSearchMethods.ContainsQuery(c.Name, searchQueries)).Skip(toSkip).Take(toTake);
                toSkip = Math.Clamp(toSkip - _context.Categories.Count(), 0, toSkip);
                toTake -= entities.Categories.Count();

                entities.Users = (_mapper.Map<IEnumerable<UserSearchDto>>(_users))
                    .Where(u => AdditionalSearchMethods.ContainsQuery(u.UserName, searchQueries)).Skip(toSkip).Take(toTake);
                toSkip = Math.Clamp(toSkip - _context.Users.Count(), 0, toSkip);
                toTake -= entities.Users.Count();

                entities.Books = (_mapper.Map<IEnumerable<BookDto>>(_books))
                    .Where(b => AdditionalSearchMethods.ContainsQuery(b.Title, searchQueries)).Skip(toSkip).Take(toTake);
            }
            else
            {
                entities.Authors = (_mapper.Map<IEnumerable<AuthorDto>>(_authors))
                    .Where(a => AdditionalSearchMethods.ContainsQuery(a.Name, searchQueries));

                entities.Categories = (_mapper.Map<IEnumerable<CategoryDto>>(_categories))
                    .Where(c => AdditionalSearchMethods.ContainsQuery(c.Name, searchQueries));

                entities.Users = (_mapper.Map<IEnumerable<UserSearchDto>>(_users))
                    .Where(u => AdditionalSearchMethods.ContainsQuery(u.UserName, searchQueries));

                entities.Books = (_mapper.Map<IEnumerable<BookDto>>(_books))
                    .Where(b => AdditionalSearchMethods.ContainsQuery(b.Title, searchQueries));
            }

            var quantity = CountItems(searchQueries);

            return new DataDto<SearchAll>() { singleData = entities, count = quantity };
        }

        private int CountItems(string[] searchQueries)
        {
            var count = _mapper.Map<IEnumerable<AuthorDto>>(_authors)
                .Where(a => AdditionalSearchMethods.ContainsQuery(a.Name, searchQueries)).Count();
            count += _mapper.Map<IEnumerable<CategoryDto>>(_categories)
                .Where(c => AdditionalSearchMethods.ContainsQuery(c.Name, searchQueries)).Count();
            count += _mapper.Map<IEnumerable<UserSearchDto>>(_users)
                .Where(u => AdditionalSearchMethods.ContainsQuery(u.UserName, searchQueries)).Count();
            count += _mapper.Map<IEnumerable<BookDto>>(_books)
                .Where(b => AdditionalSearchMethods.ContainsQuery(b.Title, searchQueries)).Count();

            return count;
        }

        private void SortQueries(SortType? sort)
        {
            switch (sort)
            {
                default:
                case SortType.ByName:
                    _users = _context.Users.OrderBy(u => u.UserName);
                    _categories = _context.Categories.OrderBy(a => a.Name);
                    _authors = _context.Authors.OrderBy(a => a.Name);
                    break;
                case SortType.ByNameDescending:
                    _users = _context.Users.OrderByDescending(u => u.UserName);
                    _categories = _context.Categories.OrderByDescending(a => a.Name);
                    _authors = _context.Authors.OrderByDescending(a => a.Name);
                    break;
            }
        }
    }
}
