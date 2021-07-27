using Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class PhotoUploadRequest : IRequest
    {
        [Required]
        public string TypeId { get; set; }
        [Required]
        public string Path { get; set; }
    }
}
