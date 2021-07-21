using Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class CategoryRequest : IRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
