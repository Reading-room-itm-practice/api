using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;

namespace WebAPI.DTOs
{
    public class PhotoResponseDto : IResponseDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Path { get; set; }
    }
}
