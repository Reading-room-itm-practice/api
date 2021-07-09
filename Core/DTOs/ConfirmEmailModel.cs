using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class ConfirmEmailModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
