using Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Requests
{
    public class PhotoUploadRequest : IRequest
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public string Path { get; set;}
    }
}
