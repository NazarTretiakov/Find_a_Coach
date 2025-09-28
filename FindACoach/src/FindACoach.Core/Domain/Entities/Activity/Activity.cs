using FindACoach.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.Activity
{
    public abstract class Activity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [StringLength(400)]
        public string? ImagePath { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
