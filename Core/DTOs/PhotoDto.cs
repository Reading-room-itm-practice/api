using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class PhotoDto : IDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Path { get; set; }
    }
}
