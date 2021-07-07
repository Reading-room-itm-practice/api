using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;

namespace WebAPI.DTOs
{
    public class PhotoUpdateDto : IRequestDto
    {
        public int? BookId { get; set; }
    }
}
