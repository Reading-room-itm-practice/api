using Storage.Identity;
using System;

namespace Storage.Models.Photos
{
    public class ProfilePhoto : Photo
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
