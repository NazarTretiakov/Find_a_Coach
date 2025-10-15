using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.User
{
    public class Certification
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Domain.IdentityEntities.User? User { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string IssuingOrganization { get; set; } = string.Empty;

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        [StringLength(400)]
        public string? ImagePath { get; set; } = string.Empty;

        [StringLength(200)]
        public string CredentialId { get; set; } = string.Empty;

        [StringLength(2048)]
        [Url(ErrorMessage = "Invalid website URL format.")]
        public string CredentialUrl { get; set; } = string.Empty;

        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    }
}
