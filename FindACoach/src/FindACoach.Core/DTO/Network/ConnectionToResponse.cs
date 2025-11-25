namespace FindACoach.Core.DTO.Network
{
    public class ConnectionToResponse
    {
        public Guid ConnectedUserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Headline { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
