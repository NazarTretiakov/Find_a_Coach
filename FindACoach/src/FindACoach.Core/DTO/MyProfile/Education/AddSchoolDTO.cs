using FindACoach.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile.Education
{
    public class AddSchoolDTO
    {
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string SchoolName { get; set; } = string.Empty;

        [Required]
        public string Degree { get; set; } = string.Empty;

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string FieldOfStudy { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(40)]
        public string? Location { get; set; }

        public List<string> SkillTitles { get; set; } = new List<string>();
    }
}
