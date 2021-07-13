using Core.Common;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class CategoryDto : IDto, ISearchable
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
