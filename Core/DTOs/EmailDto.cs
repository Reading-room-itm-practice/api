using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class EmailDto
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
