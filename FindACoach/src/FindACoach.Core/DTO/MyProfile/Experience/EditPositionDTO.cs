using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile.Experience
{
    public class EditPositionDTO
    {
        [Required]
        public string PositionId { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [RegularExpression("full-time|part-time|self-employed|internship", ErrorMessage = "Invalid employment type.")]
        public string EmploymentType { get; set; }

        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [Required]
        public bool IsCurrentlyWorkingHere { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(40)]
        public string? Location { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        public List<string> SkillTitles { get; set; }
    }
}
