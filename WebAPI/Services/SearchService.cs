using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Identity;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class SearchService : ISearchService
    {
        private readonly ICategoryService categoryService;
        private readonly ICrudService<Author> _authors;
        private readonly ICrudService<Book> _books;
        private readonly UserManager<Identity.User> _users;

        public SearchService(ICategoryService categoryService, ICrudService<Author> authors, ICrudService<Book> books, UserManager<Identity.User> users)
        {
            this.categoryService = categoryService;
            _authors = authors;
            _books = books;
            _users = users;
        }

        private bool ContainsQuerry(string name, string[] querries)
        {
            foreach (string querry in querries)
            {
                if (name.ToUpper().Contains(querry.ToUpper())) return true;
            }
            return false;
        }

        //Book specific search
        private bool ContainsQuerryBook(BookResponseDto book, string[] querries)
        {
            foreach (string querry in querries)
            {
                if (book.Title.ToUpper().Contains(querry.ToUpper())) return true;
                else if (_authors.GetById<Author>(book.AuthorId).Result.Name.ToUpper().Contains(querry.ToUpper())) return true;
            }
            return false;
        }
        

        private string[] ProcessSearchString(string searchString)
        {
            searchString = Regex.Replace(searchString, @"\s+", " "); //change multiple spaces into single ones
            return searchString.Split(" "); //split searchString into single querries by space
        }

        //DELETE(?)
        //private int ByMatch(string[] searchQuerries, string _name)
        //{
        //    string[] names = ProcessSearchString(_name);
        //    int count = 0;
        //    foreach(string name in names)
        //        foreach(string querry in searchQuerries)
        //            if(name.ToUpper().Contains(querry.ToUpper()))
        //                count = (count < querry.Length) ? querry.Length : count;
        //    return count;
        //}


        //Change to CategoryResponseDto
        public IEnumerable<CategoryDTO> SearchCategory(string searchString, SortType? sort)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var categories = categoryService.GetCategories().Result //_getter.GetAll()
                .Where(c => ContainsQuerry(c.Name, searchQuerries));
                

            switch (sort)
            {
                default:
                case SortType.byName:
                    categories = categories.OrderBy(c => c.Name);
                    break;
                case SortType.byId:
                    categories = categories.OrderBy(c => c.id);
                    break;
                //case SortType.byMatch:
                //    categories = categories.OrderBy(c => ByMatch(searchQuerries, c.Name));
                //    break;
            }

            return categories;
        }

        public IEnumerable<AuthorResponseDto> SearchAuthor(string searchString, SortType? sort)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var authors = _authors.GetAll<AuthorResponseDto>().Result.Where(a => ContainsQuerry(a.Name, searchQuerries));

            switch (sort)
            {
                default:
                case SortType.byName:
                    authors = authors.OrderBy(a => a.Name);
                    break;
            }
            return authors;
        }

        public IEnumerable<BookResponseDto> SearchBook(string searchString, SortType? sort)
        {
            var searchQuerries = ProcessSearchString(searchString);
            var books = _books.GetAll<BookResponseDto>().Result.Where(b => ContainsQuerryBook(b, searchQuerries));

            switch (sort)
            {
                default:
                case SortType.byName:
                    books = books.OrderBy(b => b.Title);
                    break;
            }
            return books; 
        }
  

       public async Task<IQueryable<User>> SearchUser(string searchString, SortType? sort)
       {
            var searchQuerries = ProcessSearchString(searchString);
            var users = _users.Users.Where(u => ContainsQuerry(u.UserName, searchQuerries));

           switch (sort)
           {
               default:
               case SortType.byName:
                   users = users.OrderBy(u => u.UserName);
                   break;
           }
           return users; 
       }
      
    }

    public enum SortType
    {
        byName,
        byId,
        byAuthor,
        byScore,
    };
}
