using Storage.Identity;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTests
{
    public class Database
    {
        public List<Author> Authors { get; set; }
        public List<Book> Books { get; set; }
        public List<Category> Categories { get; set; }
        public List<User> Users { get; set; }

        public Database()
        {
            Authors = new List<Author>();
            Books = new List<Book>();
            Categories = new List<Category>();
            Users = new List<User>();
            Seed();
        }

        private void Seed()
        {
            Users.Add(new User { Id = new Guid("7020d777-39ee-486e-0c3d-08d9622e5cf6"), UserName = "Tester" });
            Books.Add(new Book { Title = "Dave is dave", Description = "Like an orange", ReleaseDate = DateTime.UtcNow });
            Authors.Add(new Author { Name = "Simon", Biography = "He's alive" });
            Categories.Add(new Category { Name = "Piece" });
        }
    }
}
