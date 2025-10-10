namespace FindACoach.Core.DTO.MyProfile.Experience
{
    public class PositionToResponse
    {
        public Guid PositionId { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string EmploymentType { get; set; }
        public string Location { get; set; }
        public bool IsCurrentlyWorkingHere { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public List<string> SkillTitles { get; set; }
    }
}
