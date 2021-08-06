using Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests
{
    public class ReviewCommentRequest : IRequest
    {
        [Required]
        public int ReviewId { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }
    }
}
