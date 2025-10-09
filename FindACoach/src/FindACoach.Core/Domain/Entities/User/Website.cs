using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities
{
    public class Website
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public IdentityEntities.User User { get; set; }

        [Required]
        [StringLength(2048)]
        public string Url { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Type { get; set; } = string.Empty;
    }
}
