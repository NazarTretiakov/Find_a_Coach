using FindACoach.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities
{
    public class Website
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        [StringLength(200)]
        public string Url { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Type { get; set; } = string.Empty;
    }
}
