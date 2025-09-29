using FindACoach.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile.Activities
{
    public class AddActivityDTO
    {
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        public DateTime? BeginningDate { get; set; }

        public ActivityType ActivityType { get; set; }

        public IFormFile? Image { get; set; }

        [StringLength(200, MinimumLength = 3)]
        public string Description { get; set; }

        public List<SubjectDTO>? Subjects{ get; set; } = new List<SubjectDTO>();

        public List<SearchPersonPanelDTO> SearchPersonPanels { get; set; } = new List<SearchPersonPanelDTO>();

        public List<SurveyOptionDTO> SurveyOptions { get; set; } = new List<SurveyOptionDTO>();
    }
}