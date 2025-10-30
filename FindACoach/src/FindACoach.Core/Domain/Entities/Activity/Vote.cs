using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.Activity
{
    public class Vote
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public IdentityEntities.User? User { get; set; }

        [Required]
        public Guid SurveyOptionId { get; set; }
        public SurveyOption SurveyOption { get; set; }
    }
}
