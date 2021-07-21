using Storage.Models;
using System.Collections.Generic;

namespace Storage.Iterfaces
{
    public interface IFollowable
    {
        public ICollection<Follow> Followers { get; set; }
    }
}
