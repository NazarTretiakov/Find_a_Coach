namespace FindACoach.Core.Domain.Entities.Activity
{
    public class QA : Activity
    {
        public ICollection<QAAnswer> Answers { get; set; }
    }
}
