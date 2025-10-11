using FindACoach.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.User
{
    public class School
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public IdentityEntities.User? User { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string SchoolName { get; set; }

        [Required]
        public DegreeOptions Degree { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string FieldOfStudy { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(40)]
        public string? Location { get; set; }

        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    }
}
