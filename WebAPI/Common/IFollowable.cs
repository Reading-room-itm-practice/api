﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Common
{
    public interface IFollowable
    {
        public ICollection<Follow> Followers { get; set; }
    }
}