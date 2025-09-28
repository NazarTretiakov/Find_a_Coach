using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.Activity
{
    public class SurveyOption
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Inscription { get; set; } = string.Empty;

        public Guid SurveyId { get; set; }
        public Survey Survey { get; set; }
    }
}
