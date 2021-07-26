using Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class CategoryRequest : IRequest
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }
    }
}
