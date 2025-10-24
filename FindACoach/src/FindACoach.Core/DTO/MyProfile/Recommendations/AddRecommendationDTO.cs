using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile.Recommendations
{
    public class AddRecommendationDTO
    {
        [Required]
        public string RecommenderUserId { get; set; } = string.Empty;

        [Required]
        public string RecommendedUserId { get; set; } = string.Empty;

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Content { get; set; } = string.Empty;
    }
}