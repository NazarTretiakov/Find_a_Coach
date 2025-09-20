namespace FindACoach.Core.DTO.MyProfile
{
    public class PersonalAndContactInformationToResponse
    {
        public string ProfileImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrimaryOccupation { get; set; }
        public string Headline { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public List<WebsiteDTO> Websites { get; set; }
    }
}
