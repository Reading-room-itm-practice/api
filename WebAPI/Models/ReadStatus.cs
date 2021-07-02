﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;

namespace WebAPI.Models
{
    [Table("read_statuses")]
    public class ReadStatus : IDbModel
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public bool IsRead { get; set; }
        public bool IsWantRead { get; set; }
        public bool IsFavorite { get; set; }
    }
}
