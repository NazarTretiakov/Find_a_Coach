using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile.Activities
{
    public class SurveyOptionDTO
    {
        [StringLength(100, MinimumLength = 2)]
        public string Inscription { get; set; }
    }
}
