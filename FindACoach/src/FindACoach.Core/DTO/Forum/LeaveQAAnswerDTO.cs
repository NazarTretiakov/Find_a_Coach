using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.Forum
{
    public class LeaveQAAnswerDTO
    {
        [Required]
        public string QAId { get; set; }

        [Required]
        public string CreatorId { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; }
    }
}
