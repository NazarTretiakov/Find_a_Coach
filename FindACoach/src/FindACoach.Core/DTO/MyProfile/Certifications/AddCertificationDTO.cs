using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile.Certifications
{
    public class AddCertificationDTO
    {
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string IssuingOrganization { get; set; } = string.Empty;

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public IFormFile? Image { get; set; }

        [StringLength(200)]
        public string CredentialId { get; set; } = string.Empty;

        [StringLength(2048)]
        [Url(ErrorMessage = "Invalid website URL format.")]
        public string CredentialUrl { get; set; } = string.Empty;

        public List<string> SkillTitles { get; set; } = new List<string>();
    }
}
