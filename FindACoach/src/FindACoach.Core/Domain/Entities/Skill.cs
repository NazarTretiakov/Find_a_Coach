using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.Entities.User;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities
{
    public class Skill
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        public ICollection<IdentityEntities.User> Users { get; set; } = new List<IdentityEntities.User>();
        public ICollection<SearchPersonPanel> Panels { get; set; } = new List<SearchPersonPanel>();
        public ICollection<Position> Positions { get; set; } = new List<Position>();
        public ICollection<School> Schools { get; set; } = new List<School>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
