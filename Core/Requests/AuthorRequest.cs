using Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class AuthorRequest : IRequest
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public string Biography { get; set; }
    }
}
