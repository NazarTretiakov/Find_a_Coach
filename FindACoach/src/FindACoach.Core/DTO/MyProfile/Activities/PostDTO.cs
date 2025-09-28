using FindACoach.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile.Activities
{
    public class PostDTO
    {
        public string Title { get; set; }
        public IFormFile? Image { get; set; }
        public string? Description { get; set; }
        public List<SubjectDTO>? Subjects { get; set; } = new List<SubjectDTO>();

    }
}
