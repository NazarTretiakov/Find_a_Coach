using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.Activity
{
    public class EventApplication
    {
        [Key]
        public Guid Id { get; set; }

        public Guid SearchPersonPanelId { get; set; }
        public SearchPersonPanel SearchPersonPanel { get; set; }

        public Guid UserId { get; set; }
        public IdentityEntities.User User { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }
    }
}
