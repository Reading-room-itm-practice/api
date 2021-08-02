using Storage.Models.Likes;
using System.Collections.Generic;

namespace Storage.Interfaces
{
    public interface ILikeable<T> where T : Like
    {
        public ICollection<T> Likes { get; set; }      
    }
}
