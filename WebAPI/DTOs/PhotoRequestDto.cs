using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;

namespace WebAPI.DTOs
{
    public class PhotoRequestDto : IRequestDto
    {
        public int? BookId { get; set; }
        [Required]
        public string Path { get; set;}
        public PhotoRequestDto(string path, int? bookId) 
        { 
            Path = path;
            BookId = (bookId == null) ? null : bookId;
        }
    }
}
