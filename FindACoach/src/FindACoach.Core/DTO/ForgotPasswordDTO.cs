using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO
{
    public class ForgotPasswordDTO
    {
        [Required]
        public string Email { get; set; }
    }
}
