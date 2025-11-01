using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.Network
{
    public class ConnectionRequestIdDTO
    {
        [Required]
        public string ConnectionId { get; set; } = string.Empty;
    }
}
