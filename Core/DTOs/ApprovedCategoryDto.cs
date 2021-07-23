using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ApprovedCategoryDto : CategoryDto
    {
        public bool Approved { get; set; }
    }
}
