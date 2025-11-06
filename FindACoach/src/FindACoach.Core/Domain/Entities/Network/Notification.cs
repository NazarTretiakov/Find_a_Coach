using FindACoach.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.Network
{
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public IdentityEntities.User? User { get; set; }

        [Required]
        public Guid NotifiedObjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfCreation { get; set; }

        [Required]
        public NotificationType Type { get; set; }
    }
}
