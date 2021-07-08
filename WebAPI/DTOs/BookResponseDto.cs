using System.Collections.Generic;
using Core.Common;

namespace WebAPI.DTOs
{
    public class BookResponseDto : IResponseDto
    {
        public string Id { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int MainPhotoId { get; set; }
        public ICollection<int> PhotosId { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
    }
}
