using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.Activity
{
    public class QAAnswer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; } = string.Empty;

        public Guid QAId { get; set; }
        public QA QA { get; set; }
    }
}
