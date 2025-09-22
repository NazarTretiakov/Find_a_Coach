using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.Authentication
{
    public class ForgotPasswordDTO
    {
        [Required]
        public string Email { get; set; }
    }
}
