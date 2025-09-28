using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile.Activities
{
    public class SearchPersonPanelDTO
    {
        [StringLength(30, MinimumLength = 2)]
        public string PositionName { get; set; }

        [StringLength(160, MinimumLength = 2)]
        public string? PreferredSkills { get; set; }

        [StringLength(20)]
        public string? Payment { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }
    }
}
