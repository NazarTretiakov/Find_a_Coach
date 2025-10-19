using FindACoach.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.User
{
    public class Language
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public IdentityEntities.User? User { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        public LanguageProficiency LanguageProficiency { get; set; }
    }
}
