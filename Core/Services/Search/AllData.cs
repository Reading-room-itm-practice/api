using Storage.Identity;
using Storage.Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services.Search
{
    public class AllData
    {
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Book> Books { get; set; }

        public int Count()
        {
            int count = Authors.Count() + Users.Count() + Categories.Count() + Books.Count();

            return count;
        }

    }
}
