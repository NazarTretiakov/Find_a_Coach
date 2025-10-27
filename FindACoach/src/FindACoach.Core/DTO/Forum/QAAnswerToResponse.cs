namespace FindACoach.Core.DTO.Forum
{
    public class QAAnswerToResponse
    {
        public string Content { get; set; } = string.Empty;
        public Guid QAId { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string CreatorFirstName { get; set; } = string.Empty;
        public string CreatorLastName { get; set; } = string.Empty;
        public string CreatorProfileImagePath { get; set; } = string.Empty;
    }
}
