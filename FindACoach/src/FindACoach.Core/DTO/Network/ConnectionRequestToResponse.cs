namespace FindACoach.Core.DTO.Network
{
    public class ConnectionRequestToResponse
    {
        public Guid ConnectionId { get; set; }
        public Guid UserId { get; set; }
        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public string UserImagePath { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime DateOfCreation { get; set; }
    }
}