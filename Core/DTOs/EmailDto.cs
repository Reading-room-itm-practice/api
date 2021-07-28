using Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class EmailDto : IDto
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
