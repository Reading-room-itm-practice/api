using Core.Common;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ReviewDto : IDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Stars { get; set; }
        public string Content { get; set; }
        public ICollection<int> CommentsIds { get; set; }
    }
}
