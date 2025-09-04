using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.IdentityEntities
{
    public class User: IdentityUser<Guid>
    {
        [StringLength(20)]
        public string? FirstName { get; set; }

        [StringLength(20)]
        public string? LastName { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpirationDateTime { get; set; }

        public bool IsCompleteProfileWindowVisible { get; set; }

        public string CompleteProfileWindowTitle { get; set; }
    }
}
