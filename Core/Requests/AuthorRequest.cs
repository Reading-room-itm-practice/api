using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;

namespace Core.Requests
{
    public class AuthorRequest : IRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Biography { get; set; }
    }
}
