﻿using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ReviewCommentDto : IDto
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public string Content { get; set; }
        public UserDto Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
