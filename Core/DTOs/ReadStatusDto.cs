using Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ReadStatusDto
    {
        public bool IsRead { get; set; }
        public bool IsWantRead { get; set; }
        public bool IsFavorite { get; set; }
    }
}
