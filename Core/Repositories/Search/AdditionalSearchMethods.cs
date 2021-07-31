using Core.DTOs;
using Core.Enums;
using Core.Services;
using Storage.Identity;
using Storage.Interfaces;
using Storage.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Repositories.Search
{
    class AdditionalSearchMethods
    {
        public static bool ContainsQuery(string name, string[] queries)
        {
            if (queries.All(q => name.ToUpper().Contains(q.ToUpper()))) return true;
            return false;
        }

        public static string[] ProcessSearchString(string searchString)
        {
            searchString ??= "";
            searchString = Regex.Replace(searchString, @"\s+", " ");
            return searchString.Split(" ");
        }

        public static IQueryable<T> SortGeneric<T>(IQueryable<T> sorted, SortType? sort) where T : class
        {
            switch (sort)
            {
                case SortType.ByName:
                    if (typeof(T) == typeof(Category))
                    {
                        var categories = sorted as IQueryable<Category>;
                        sorted = (IQueryable<T>)categories.OrderBy(a => a.Name);
                    }  
                    else if(typeof(T) == typeof(Author))
                    {
                        var authors = sorted as IQueryable<Author>;
                        sorted = (IQueryable<T>)authors.OrderBy(a => a.Name);
                    }
                    else if (typeof(T) == typeof(User))
                    {
                        var users = sorted as IQueryable<User>;
                        sorted = (IQueryable<T>)users.OrderBy(u => u.UserName);
                    }
                    break;
                case SortType.ByNameDescending:
                    if (typeof(T) == typeof(Category))
                    {
                        var categories = sorted as IQueryable<Category>;
                        sorted = (IQueryable<T>)categories.OrderByDescending(a => a.Name);
                    }
                    else if (typeof(T) == typeof(Author))
                    {
                        var authors = sorted as IQueryable<Author>;
                        sorted = (IQueryable<T>)authors.OrderByDescending(a => a.Name);
                    }
                    else if (typeof(T) == typeof(User))
                    {
                        var users = sorted as IQueryable<User>;
                        sorted = (IQueryable<T>)users.OrderByDescending(u => u.UserName);
                    }
                    break;
            }
            return sorted;
        }

        public static IQueryable<Book> SortBooks(IQueryable<Book> books, SortType? sort)
        {
            switch (sort)
            {
                default:
                case SortType.ByName:
                    books = books.OrderBy(b => b.Title);
                    break;
                case SortType.ByNameDescending:
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case SortType.ByRelaseYear:
                    books = books.OrderBy(b => b.ReleaseDate.Value.Year);
                    break;
                case SortType.ByRelaseYearDescending:
                    books = books.OrderByDescending(b => b.ReleaseDate.Value.Year);
                    break;
            }
            return books;
        }

        public static DataDto<IEnumerable<T>> Pagination<T>(PaginationFilter filter, IEnumerable<T> data) where T : class
        {
            filter.Valid();
            var count = data.Count();
            if (filter.PageSize != 0)
            {
                return new DataDto<IEnumerable<T>>()
                {
                    Data = data
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize),
                    Quantity = count
                };
            }

            return new DataDto<IEnumerable<T>>() { Data = data, Quantity = count };
        }

    }
}
