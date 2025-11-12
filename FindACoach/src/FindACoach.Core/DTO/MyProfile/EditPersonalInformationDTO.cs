using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile
{
    public class EditPersonalInformationDTO
    {
        public IFormFile? ProfileImage { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string Firstname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string Lastname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Primary occupation is required.")]
        [StringLength(100, ErrorMessage = "Primary occupation cannot exceed 100 characters.")]
        public string PrimaryOccupation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Headline is required.")]
        [StringLength(100, ErrorMessage = "Headline cannot exceed 100 characters.")]
        public string Headline { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        public string Location { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; } = string.Empty;

        public List<WebsiteDTO> Websites { get; set; } = new List<WebsiteDTO>();
    }

    public class WebsiteDTO
    {
        [Required(ErrorMessage = "Website URL is required.")]
        public string Url { get; set; } = string.Empty;

        [Required(ErrorMessage = "Website type is required.")]
        [RegularExpression("company|personal|portfolio|other", ErrorMessage = "Invalid website type.")]
        public string Type { get; set; } = "other";
    }
}