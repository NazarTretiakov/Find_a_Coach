using FindACoach.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.IdentityEntities
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string? LastName { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpirationDateTime { get; set; }

        [Required]
        public bool IsCompleteProfileWindowVisible { get; set; }

        [Required]
        [StringLength(100)]
        public string CompleteProfileWindowTitle { get; set; } = string.Empty;

        [StringLength(400)]
        public string ImagePath { get; set; } = string.Empty;

        [StringLength(30)]
        public string PrimaryOccupation { get; set; } = string.Empty;

        [StringLength(100, MinimumLength = 4)]
        public string Headline { get; set; } = string.Empty;

        [StringLength(50, MinimumLength = 4)]
        public string Location { get; set; } = string.Empty;

        [StringLength(9, MinimumLength = 9)]
        public string Phone { get; set; } = string.Empty;

        public string AboutMe { get; set; } = string.Empty;

        public ICollection<Website> Websites { get; set; } = new List<Website>();

        public ICollection<Connection> Connections { get; set; } = new List<Connection>();

        public ICollection<Entities.Activity.Activity> Activities { get; set; } = new List<Entities.Activity.Activity>();

        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    }
}
