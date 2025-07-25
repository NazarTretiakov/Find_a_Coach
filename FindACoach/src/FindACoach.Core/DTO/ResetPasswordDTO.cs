﻿using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO
{
    public class ResetPasswordDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
