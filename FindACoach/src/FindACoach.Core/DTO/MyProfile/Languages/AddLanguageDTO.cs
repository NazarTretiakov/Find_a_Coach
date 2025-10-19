using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile.Languages
{
    public class AddLanguageDTO
    {
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [RegularExpression("A1|A2|B1|B2|C1|C2", ErrorMessage = "Invalid language proficiency type.")]
        public string Proficiency { get; set; }
    }
}
