namespace FindACoach.Core.DTO.Forum
{
    public class SurveyToResponse : ActivityToResponse
    {
        public List<SurveyOptionToResponse> Options { get; set; } = new List<SurveyOptionToResponse>();
    }
}
