using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage.Models;

namespace Storage.Iterfaces
{
    public interface IFollowable
    {
        public ICollection<Follow> Followers { get; set; }
    }
}
