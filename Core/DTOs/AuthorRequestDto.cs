using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;

namespace Core.DTOs
{
    public class AuthorRequestDto : IRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Biography { get; set; }
    }
}
