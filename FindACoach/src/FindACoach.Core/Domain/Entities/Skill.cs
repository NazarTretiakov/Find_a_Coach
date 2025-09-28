using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.IdentityEntities;
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

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<SearchPersonPanel> Panels { get; set; } = new List<SearchPersonPanel>();
    }
}
