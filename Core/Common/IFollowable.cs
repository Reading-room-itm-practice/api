using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Common
{
    public interface IFollowable
    {
        public ICollection<Follow> Followers { get; set; }
    }
}
