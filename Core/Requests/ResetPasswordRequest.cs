using Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@SettingsVariables.PasswordExpression,
            ErrorMessage = "Password is not valid (at least one special sign, digit, upper letter and lenght >= 8)")]
        public string newPassword { get; set; }
    }
}
