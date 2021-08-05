using Core.Common;

namespace Core.Requests
{
    public class ReadStatusRequest : IRequest
    {
        public int BookId { get; set; }
        public bool IsRead { get; set; }
        public bool IsWantRead { get; set; }
        public bool IsFavorite { get; set; }
    }
}
