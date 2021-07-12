using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class ResetPasswordDto
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*0-9]{7,}$",
            ErrorMessage = "Password is not valid (at least one special sign, digit, upper letter and lenght >= 7)")]
        public string newPassword { get; set; }
    }
}
