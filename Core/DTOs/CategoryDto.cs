﻿using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class CategoryDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
