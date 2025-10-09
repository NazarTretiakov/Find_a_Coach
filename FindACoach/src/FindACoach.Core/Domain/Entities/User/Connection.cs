using FindACoach.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindACoach.Core.Domain.Entities
{
    public class Connection
    {
        [Key]
        public Guid Id { get; set; }


        [Required]
        public Guid UserId { get; set; }
        public IdentityEntities.User User { get; set; }

        [Required]
        public Guid ConnectedUserId { get; set; }

        [ForeignKey("ConnectedUserId")]
        public IdentityEntities.User ConnectedUser { get; set; }

        [Required]
        public ConnectionStatus Status { get; set; } = ConnectionStatus.Pending;
    }
}
