﻿using Storage.Iterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Authors")]
    public class Author : AuditableModel, IDbModel, IDbMasterKey, IFollowable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Follow> Followers { get; set; }
    }
}
