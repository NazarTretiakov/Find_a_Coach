namespace FindACoach.Core.DTO.MyProfile.Activities
{
    public class ActivityCardToResponse
    {
        public Guid Id { get; set; }
        public string ImageNameOfCreator { get; set; } = string.Empty;
        public string FirstNameOfCreator { get; set; } = string.Empty;
        public string LastNameOfCreator { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ActivityType { get; set; } = string.Empty;
    }
}
