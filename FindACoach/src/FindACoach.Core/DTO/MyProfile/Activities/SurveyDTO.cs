using Microsoft.AspNetCore.Http;

namespace FindACoach.Core.DTO.MyProfile.Activities
{
    public class SurveyDTO
    {
        public string Title { get; set; }
        public IFormFile? Image { get; set; }
        public string? Description { get; set; }
        public List<SubjectDTO>? Subjects { get; set; } = new List<SubjectDTO>();
        public List<SurveyOptionDTO> SurveyOptions { get; set; } = new List<SurveyOptionDTO>();
    }
}
