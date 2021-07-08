using System.Collections.Generic;
using Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class BookRequestDto : IRequestDto
    {
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int MainPhotoId { get; set; }
        public ICollection<int> PhotosId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
