using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;

namespace WebAPI.DTOs
{
    public class UpdateAuthorDto : IRequestDto
    {
        public string Name { get; set; }
        public string Biography { get; set; }
    }
}
