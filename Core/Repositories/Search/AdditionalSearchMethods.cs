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

        public static IQueryable<T> SortGeneric<T>(IQueryable<T> sorted, SortType? sort) where T : INameSortable
        {
            switch (sort)
            {
                default:
                case SortType.ByName:
                    sorted = sorted.OrderBy(a => a.Name);
                    break;
                case SortType.ByNameDescending:
                    sorted = sorted.OrderByDescending(a => a.Name);
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
                    books = books.OrderBy(b => b.RelaseDate.Value.Year);
                    break;
                case SortType.ByRelaseYearDescending:
                    books = books.OrderByDescending(b => b.RelaseDate.Value.Year);
                    break;
            }
            return books;
        }

        public static IQueryable<User> SortUsers(IQueryable<User> users, SortType? sort)
        {
            switch (sort)
            {
                default:
                case SortType.ByName:
                    users = users.OrderBy(u => u.UserName);
                    break;
                case SortType.ByNameDescending:
                    users = users.OrderByDescending(u => u.UserName);
                    break;
            }
            return users;
        }

        public static DataDto<T> Pagination<T>(PaginationFilter filter, IEnumerable<T> data) where T : class
        {
            filter.Valid();
            var count = data.Count();
            if (filter.PageSize != 0)
            {
                return new DataDto<T>()
                {
                    data = data
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize),
                    count = count
                };
            }

            return new DataDto<T>() { data = data, count = count };
        }

    }
}
