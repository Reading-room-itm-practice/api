using Core.Common;

namespace Core.DTOs
{
    public abstract class LikeableDto : IDto
    {
        public int LikesCount { get; set; }
        public bool IsLoggedUserLike { get; set; }
    }
}
