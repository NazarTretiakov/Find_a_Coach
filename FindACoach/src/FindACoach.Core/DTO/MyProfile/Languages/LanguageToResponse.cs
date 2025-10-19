namespace FindACoach.Core.DTO.MyProfile.Languages
{
    public class LanguageToResponse
    {
        public Guid LanguageId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Proficiency { get; set; } = string.Empty;
    }
}
