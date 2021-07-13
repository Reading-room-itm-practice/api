using Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Requests
{
    public class CategoryRequest : IRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
