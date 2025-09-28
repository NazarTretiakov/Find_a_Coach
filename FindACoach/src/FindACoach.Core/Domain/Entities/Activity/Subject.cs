using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.Activity
{
    public class Subject
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
