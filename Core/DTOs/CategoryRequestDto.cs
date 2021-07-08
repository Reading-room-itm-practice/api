using Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class CategoryRequestDto : IRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
