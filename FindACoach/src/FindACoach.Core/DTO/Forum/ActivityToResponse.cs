namespace FindACoach.Core.DTO.Forum
{
    public abstract class ActivityToResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserImagePath { get; set; } = string.Empty;
        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? ImagePath { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<string> Subjects { get; set; } = new List<string>();
        public bool IsLiked { get; set; }
        public int NumberOfLikes { get; set; }
        public bool IsSaved { get; set; }
        public List<CommentToResponse> Comments { get; set; } = new List<CommentToResponse>();
    }
}