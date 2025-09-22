using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.Authentication
{
    public class TokenModel
    {
        [Required]
        public string? JwtToken { get; set; }

        [Required]
        public string? RefreshToken { get; set; }
    }
}
