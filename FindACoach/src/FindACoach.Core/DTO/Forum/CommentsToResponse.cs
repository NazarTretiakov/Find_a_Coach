namespace FindACoach.Core.DTO.Forum
{
    public class CommentsToResponse
    {
        public List<CommentToResponse> Comments { get; set; }
        public bool IsMoreCommentsLeft { get; set; }
    }
}
