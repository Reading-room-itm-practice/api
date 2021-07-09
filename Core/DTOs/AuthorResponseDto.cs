using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Core.Mappings;

namespace Core.DTOs
{
    public class AuthorResponseDto : IResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
    }
}
