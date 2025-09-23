using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile
{
    public class AboutMeDTO
    {
        [StringLength(2000)]
        public string? AboutMe { get; set; }
    }
}
