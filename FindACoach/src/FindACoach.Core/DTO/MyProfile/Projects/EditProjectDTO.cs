using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile.Projects
{
    public class EditProjectDTO
    {
        [Required]
        public string ProjectId { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string RelatedTo { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(40)]
        public string? Location { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public List<string> SkillTitles { get; set; } = new List<string>();
        public List<string> Participants { get; set; } = new List<string>();
    }
}
