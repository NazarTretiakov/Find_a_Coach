using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.Forum
{
    public class AddCommentDTO
    {
        public string ActivityId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        [StringLength(400, MinimumLength = 3)]
        public string Content { get; set; } = string.Empty;
    }
}
