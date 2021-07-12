using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;

namespace WebAPI.DTOs
{
    public class PhotoUpdateDto : IRequestDto
    {
        public int BookId { get; set; }
    }
}
