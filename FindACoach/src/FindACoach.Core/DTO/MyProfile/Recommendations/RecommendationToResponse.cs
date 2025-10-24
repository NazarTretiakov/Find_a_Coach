namespace FindACoach.Core.DTO.MyProfile.Recommendations
{
    public class RecommendationToResponse
    {
        public string RecommenderUserProfileImagePath { get; set; }
        public string RecommenderUserFirstName { get; set; }
        public string RecommenderUserLastName { get; set; }
        public string RecommenderUserHeadline { get; set; }
        public Guid RecommendationId { get; set; }
        public Guid RecommenderUserId { get; set; }
        public Guid RecommendedUserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime DateOfCreation { get; set; }
    }
}
