using Core.Common;

namespace Core.DTOs.Follows
{
    public class FollowDto : IDto
    {
        public int Id { get; set; }
        public int FollowableId { get; set; }
        public string FollowableType { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
    }
}
