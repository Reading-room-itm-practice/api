﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;

namespace WebAPI.DTOs
{
    public class CategoryResponseDto : IResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
