using AutoMapper;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Search;
using Core.Services;
using Core.Services.Search;
using Storage.DataAccessLayer;
using Storage.Identity;
using Storage.Models;
using System;
using System.Linq;

namespace Core.Repositories.Search
{
    class SearchAllRepository : ISearchAllRepository
    {
        private readonly ApiDbContext _context;
        private IQueryable<User> _users;
        private IQueryable<Author> _authors;
        private IQueryable<Book> _books;
        private IQueryable<Category> _categories;

        public SearchAllRepository(ApiDbContext context)
        {
            _context = context;
        }

        public DataDto<SearchAll> SearchAll(PaginationFilter filter, string searchString, SortType? sort)
        {
            var searchQueries = AdditionalSearchMethods.ProcessSearchString(searchString);
            SortQueries(sort);
            FilterQueries(searchQueries);

            var entities = new SearchAll();
            var toSkip = (filter.PageNumber - 1) * filter.PageSize;
            var toTake = filter.PageSize;

            if (filter.PageSize != 0)
            {
                _authors = _authors.Skip(toSkip).Take(toTake);
                toSkip = Math.Clamp(toSkip -  _authors.Count(), 0, toSkip);
                toTake -= _authors.Count();

                _categories = _categories.Skip(toSkip).Take(toTake);
                toSkip = Math.Clamp(toSkip - _categories.Count(), 0, toSkip);
                toTake -= _categories.Count();

                _users = _users.Skip(toSkip).Take(toTake);
                toSkip = Math.Clamp(toSkip - _users.Count(), 0, toSkip);
                toTake -= _users.Count();

                _books = _books.Skip(toSkip).Take(toTake);

            }

            entities.Authors = _authors.ToList();
            entities.Categories = _categories.ToList();
            entities.Users = _users.ToList();
            entities.Books = _books.ToList();

            var quantity = entities.Count();

            return new DataDto<SearchAll>() { SingleData = entities, Quantity = quantity };
        }

        private void SortQueries(SortType? sort)
        {
            switch (sort)
            {
                case SortType.ByName:
                    _users = _context.Set<User>().OrderBy(u => u.UserName).AsQueryable();
                    _categories = _context.Set<Category>().OrderBy(a => a.Name);
                    _authors = _context.Set<Author>().OrderBy(a => a.Name);
                    break;
                case SortType.ByNameDescending:
                    _users = _context.Set<User>().OrderByDescending(u => u.UserName);
                    _categories = _context.Set<Category>().OrderByDescending(a => a.Name);
                    _authors = _context.Set<Author>().OrderByDescending(a => a.Name);
                    break;
                default:
                    _users = _context.Set<User>();
                    _categories = _context.Set<Category>();
                    _authors = _context.Set<Author>();
                    break;
            }
            _books = _context.Set<Book>();
        }

        private void FilterQueries(string[] searchQueries)
        {

            _authors = _authors.AsEnumerable().Where(a => AdditionalSearchMethods.ContainsQuery(a.Name, searchQueries)).AsQueryable();
            _categories = _categories.AsEnumerable().Where(c => AdditionalSearchMethods.ContainsQuery(c.Name, searchQueries)).AsQueryable();
            _users = _users.AsEnumerable().Where(u => AdditionalSearchMethods.ContainsQuery(u.UserName, searchQueries)).AsQueryable();
            _books = _books.AsEnumerable().Where(b => AdditionalSearchMethods.ContainsQuery(b.Title, searchQueries)).AsQueryable();
        }
    }
}
