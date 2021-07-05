using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class ErrorDto
    {
        public int Code { get; set; }
        public string Error { get; set; }
    }
}
