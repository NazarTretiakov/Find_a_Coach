using FindACoach.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile.Activities
{
    public class EventDTO
    {
        public string Title { get; set; }
        public DateTime? BeginningDate { get; set; }
        public IFormFile? Image { get; set; }
        public string? Description { get; set; }
        public List<SubjectDTO>? Subjects { get; set; } = new List<SubjectDTO>();
        public List<SearchPersonPanelDTO> SearchPersonPanels { get; set; } = new List<SearchPersonPanelDTO>();
    }
}
