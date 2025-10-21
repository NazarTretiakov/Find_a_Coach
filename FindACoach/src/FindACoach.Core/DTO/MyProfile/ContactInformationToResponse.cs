namespace FindACoach.Core.DTO.MyProfile
{
    public class ContactInformationToResponse
    {
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<WebsiteDTO> Websites { get; set; } = new List<WebsiteDTO>();
    }
}
