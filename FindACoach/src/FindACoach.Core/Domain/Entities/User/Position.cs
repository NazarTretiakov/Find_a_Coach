using FindACoach.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.User
{
    public class Position
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public IdentityEntities.User? User { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        public EmploymentType EmploymentType { get; set; }

        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [Required]
        public bool IsCurrentlyWorkingHere { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(40)]
        public string? Location { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    }
}
