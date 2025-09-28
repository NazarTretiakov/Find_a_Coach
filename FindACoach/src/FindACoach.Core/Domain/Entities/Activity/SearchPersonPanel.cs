using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.Activity
{
    public class SearchPersonPanel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string PositionName { get; set; } = string.Empty;

        public ICollection<Skill> PreferredSkills { get; set; } = new List<Skill>();

        [StringLength(20)]
        public string? Payment { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; } = string.Empty;

        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}
