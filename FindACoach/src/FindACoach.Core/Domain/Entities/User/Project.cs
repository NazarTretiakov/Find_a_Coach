using FindACoach.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.User
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public IdentityEntities.User? User { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public RelatedTo RelatedTo { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(40)]
        public string? Location { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public string Participants { get; set; } = string.Empty;

    }
}
