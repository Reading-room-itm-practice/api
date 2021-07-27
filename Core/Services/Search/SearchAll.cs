using Core.DTOs;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DTOs;

namespace Core.Services.Search
{
    public class SearchAll
    {
        public IEnumerable<AuthorDto> Authors { get; set; }
        public IEnumerable<UserSearchDto> Users { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }
        public IEnumerable<BookDto> Books { get; set; }

        public int Count()
        {
            int count = Authors.Count() + Users.Count() + Categories.Count() + Books.Count();

            return count;
        }
    }
}
