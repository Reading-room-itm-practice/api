using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class DataDto<T>
    {
        public IEnumerable<T> data { get; set; }
        public int count { get; set; }
    }
}
