using FindACoach.Core.Domain.Entities;
using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.IdentityEntities
{
    public class User : IdentityUser<Guid>
    {
        [StringLength(20, MinimumLength = 2)]
        public string? FirstName { get; set; }

        [StringLength(20, MinimumLength = 2)]
        public string? LastName { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpirationDateTime { get; set; }

        [Required]
        public bool IsCompleteProfileWindowVisible { get; set; }

        [Required]
        [StringLength(100)]
        public string CompleteProfileWindowTitle { get; set; } = string.Empty;

        [Required]
        [StringLength(400)]
        public string ImagePath { get; set; } = string.Empty;

        [StringLength(30)]
        public string? PrimaryOccupation { get; set; } = string.Empty;

        [StringLength(100, MinimumLength = 4)]
        public string? Headline { get; set; } = string.Empty;

        [StringLength(50, MinimumLength = 4)]
        public string? Location { get; set; } = string.Empty;

        [StringLength(9, MinimumLength = 9)]
        public string? Phone { get; set; } = string.Empty;

        public string? AboutMe { get; set; } = string.Empty;

        public ICollection<Website> Websites { get; set; } = new List<Website>();

        public ICollection<Connection> Connections { get; set; } = new List<Connection>();

        public ICollection<Position> Positions { get; set; } = new List<Position>();

        public ICollection<School> Schools { get; set; } = new List<School>();

        public ICollection<Project> Projects { get; set; } = new List<Project>();

        public ICollection<Certification> Certifications { get; set; } = new List<Certification>();

        public ICollection<Language> Languages { get; set; } = new List<Language>();

        public ICollection<Recommendation> RecommendationsReceived { get; set; } = new List<Recommendation>();

        public ICollection<Recommendation> RecommendationsGiven { get; set; } = new List<Recommendation>();

        public ICollection<Entities.Activity.Activity> Activities { get; set; } = new List<Entities.Activity.Activity>();

        public ICollection<Skill> Skills { get; set; } = new List<Skill>();

        public ICollection<Like> Likes { get; set; } = new List<Like>();
        
        public ICollection<Save> Saves { get; set; } = new List<Save>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}