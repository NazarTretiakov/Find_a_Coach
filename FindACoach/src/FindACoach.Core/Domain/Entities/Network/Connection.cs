using FindACoach.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.Network
{
    public class Connection
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public IdentityEntities.User? User { get; set; }

        [Required]
        public Guid ConnectedUserId { get; set; }
        public IdentityEntities.User? ConnectedUser { get; set; }

        [StringLength(2000)]
        public string? Message { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }

        [Required]
        public ConnectionStatus Status { get; set; } = ConnectionStatus.Pending;
    }
}
