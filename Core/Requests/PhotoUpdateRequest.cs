using Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class PhotoUpdateRequest : IRequest
    {
        [Required]
        public string TypeId { get; set; }
    }
}
