using Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class PhotoUploadRequest : IRequest
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public string Path { get; set; }
    }
}
