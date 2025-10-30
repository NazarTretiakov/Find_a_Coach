using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.Forum
{
    public class VoteInSurveyDTO
    {
        [Required]
        public string OptionId { get; set; }
    }
}
