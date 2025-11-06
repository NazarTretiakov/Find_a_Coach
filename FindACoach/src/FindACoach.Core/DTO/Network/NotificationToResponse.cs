namespace FindACoach.Core.DTO.Network
{
    public class NotificationToResponse
    {
        public Guid NotificationId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime DateOfCreation { get; set; }
        public string Type { get; set; } = string.Empty;
        public Guid NotifiedObjectId { get; set; }
        public string RelatedUserFirstName { get; set; } = string.Empty;
        public string RelatedUserLastName { get; set; } = string.Empty;
        public string RelatedUserImagePath { get; set; } = string.Empty;
    }
}
