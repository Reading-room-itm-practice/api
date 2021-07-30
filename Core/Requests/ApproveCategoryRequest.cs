using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests
{
    public class ApproveCategoryRequest : CategoryRequest
    {
        public bool Approved { get; set; }
    }
}
