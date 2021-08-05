using System;

namespace Core.DTOs.Follows
{
    public class FollowerDto 
    {
        public int Id { get; set; }
        public Guid FollowerId { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
    }
}
