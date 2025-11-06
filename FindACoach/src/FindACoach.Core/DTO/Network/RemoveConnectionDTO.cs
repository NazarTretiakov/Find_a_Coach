using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.Network
{
    public class RemoveConnectionDTO
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string ConnectedUserId { get; set; } = string.Empty;
    }
}
