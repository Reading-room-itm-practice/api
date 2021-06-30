﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;

namespace WebAPI.Models
{
    [Table("review_comments")]
    public class ReviewComment : AuditableModel, ILikeable
    {
        [Key]
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public Review Review { get; set; }
        public string Content { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
