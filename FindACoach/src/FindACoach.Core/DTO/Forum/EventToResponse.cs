namespace FindACoach.Core.DTO.Forum
{
    public class EventToResponse : ActivityToResponse
    {
        public DateTime BeginningDate { get; set; }
        public List<SearchPersonPanelToResponse> SearchPersonPanels { get; set; } = new List<SearchPersonPanelToResponse>();
    }
}
