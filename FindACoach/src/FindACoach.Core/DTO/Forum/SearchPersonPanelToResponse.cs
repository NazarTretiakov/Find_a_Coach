namespace FindACoach.Core.DTO.Forum
{
    public class SearchPersonPanelToResponse
    {
        public Guid Id { get; set; }
        public string PositionName { get; set; } = string.Empty;
        public List<string> PreferredSkills { get; set; } = new List<string>();
        public string? Payment { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
