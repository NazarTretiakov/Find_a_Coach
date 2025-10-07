namespace FindACoach.Core.DTO.Forum
{
    public class CommentToResponse
    {
        public Guid CommentId { get; set; }
        public Guid ActivityId { get; set; }
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public string UserImagePath { get; set; } = string.Empty;
        public DateTime DateOfCreation { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
