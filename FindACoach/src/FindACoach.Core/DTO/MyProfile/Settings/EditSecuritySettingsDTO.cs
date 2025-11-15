using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.DTO.MyProfile.Settings
{
    public class EditSecuritySettingsDTO
    {
        public bool IsLoginNotificationEnabled { get; set; }
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;

        [Compare("NewPassword", ErrorMessage = "New password and repeat new password do not match.")]
        public string RepeatNewPassword { get; set; } = string.Empty;
    }
}
