using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;

namespace Core.Requests
{
    public class PhotoUpdateRequest : IRequest
    {
        [Required]
        public int BookId { get; set; }
    }
}
