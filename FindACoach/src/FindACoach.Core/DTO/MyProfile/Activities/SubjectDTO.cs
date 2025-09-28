using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile.Activities
{
    public class SubjectDTO
    {
        [StringLength(30, MinimumLength = 2)]
        public string Title { get; set; }
    }
}
