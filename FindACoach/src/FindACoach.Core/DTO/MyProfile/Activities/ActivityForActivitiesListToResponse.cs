namespace FindACoach.Core.DTO.MyProfile.Activities
{
    public class ActivityForActivitiesListToResponse
    {
        public Guid Id { get; set; }
        public string ImagePathOfCreator { get; set; } = string.Empty;
        public string FirstNameOfCreator { get; set; } = string.Empty;
        public string LastNameOfCreator { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<string> Subjects { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ActivityType { get; set; } = string.Empty;
    }
}
