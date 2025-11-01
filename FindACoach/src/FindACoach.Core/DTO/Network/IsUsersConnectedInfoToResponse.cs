using FindACoach.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.Network
{
    public class IsUsersConnectedInfoToResponse
    {
        [Required]
        public bool IsUsersConnected { get; set; }

        public string? Status { get; set; }
    }
}
