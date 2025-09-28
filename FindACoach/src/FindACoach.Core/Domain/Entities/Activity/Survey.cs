namespace FindACoach.Core.Domain.Entities.Activity
{
    public class Survey : Activity
    {
        public ICollection<SurveyOption> Options { get; set; } = new List<SurveyOption>();
    }
}
