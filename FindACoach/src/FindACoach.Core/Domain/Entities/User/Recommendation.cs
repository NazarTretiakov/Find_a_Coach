using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.User
{
    public class Recommendation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid RecommenderUserId { get; set; }
        public IdentityEntities.User? RecommenderUser { get; set; }

        [Required]
        public Guid RecommendedUserId { get; set; }
        public IdentityEntities.User? RecommendedUser { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Content { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }
    }
}
