using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.Activity
{
    public class Event : Activity
    {
        [Required]
        public DateTime BeginningDate { get; set; }
        public ICollection<SearchPersonPanel> SearchPersonPanels { get; set; } = new List<SearchPersonPanel>();
    }
}
