namespace FindACoach.Core.DTO.MyProfile
{
    public class PersonalInformationToResponse
    {
        public string ProfileImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrimaryOccupation { get; set; }
        public string Headline { get; set; }
        public string Location { get; set; }
        public int ConnectionsAmount { get; set; }
        public bool IsConnected { get; set; }
        public string? ConnectionStatus{ get; set; }
    }
}
