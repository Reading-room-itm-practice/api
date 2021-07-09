using System.ComponentModel.DataAnnotations;

namespace WebAPI.Identity.Auth
{
    public class ConfirmEmailModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
