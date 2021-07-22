using Core.Common;

namespace Core.DTOs
{
    public class PhotoDto : IDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Path { get; set; }
    }
}
