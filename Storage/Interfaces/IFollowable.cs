using System.Collections.Generic;
using Storage.Models.Follows;

namespace Storage.Interfaces
{
    public interface IFollowable<T> where T : Follow
    {
    public ICollection<T> Followers { get; set; }
    }
}
