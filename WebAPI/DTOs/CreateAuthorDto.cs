using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class CreateAuthorDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Biography { get; set; }
    }
}
