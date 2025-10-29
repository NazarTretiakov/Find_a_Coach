namespace FindACoach.Core.DTO.Forum
{
    public class EventApplicationToResponse
    {
        public Guid SearchPersonPanelId { get; set; }
        public Guid UserId { get; set; }
        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public string UserProfileImagePath { get; set; } = string.Empty;
        public string UserHeadline { get; set; } = string.Empty;
        public DateTime DateOfCreation { get; set; }
    }
}
