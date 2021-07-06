﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*0-9]{7,}$", 
            ErrorMessage = "Password is not valid (at least one special sign, digit, upper letter and lenght >= 7)")]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Password doesn't match.")]
        [Required(ErrorMessage = "Confirm password is required")]
        public string ConfirmPassword { get; set; }

    }
}
