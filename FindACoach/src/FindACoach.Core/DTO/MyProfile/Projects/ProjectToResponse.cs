namespace FindACoach.Core.DTO.MyProfile.Projects
{
    public class ProjectToResponse
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string RelatedTo { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public List<string> SkillTitles { get; set; } = new List<string>();
        public List<string> Participants { get; set; } = new List<string>();
    }
}
