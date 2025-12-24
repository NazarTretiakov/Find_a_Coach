namespace FindACoach.Core.DTO.Network
{
    public class NotificationsPagedToResponse
    {
        public List<NotificationToResponse> Notifications { get; set; }
        public bool IsMoreNotificationsLeft { get; set; }
    }
}
