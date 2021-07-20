using Core.Common;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CategoryDto : IDto, INameSortable
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
