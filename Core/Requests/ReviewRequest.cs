using Core.Common;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests
{
    public class ReviewRequest : IRequest
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        [Range(1, 5)]
        public int Stars { get; set; }
        public string Content { get; set; }
        public ICollection<int> CommentsIds { get; set; }
    }
}
