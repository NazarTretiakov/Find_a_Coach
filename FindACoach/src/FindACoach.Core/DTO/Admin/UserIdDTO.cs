using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.Admin
{
    public class UserIdDTO
    {
        [Required]
        public string UserId { get; set; }
    }
}
