using FindACoach.Core.DTO.MyProfile.Settings;
using FindACoach.Core.ServiceContracts.MyProfile.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FindACoach.API.Controllers.MyProfile
{
    public class SettingsController: CustomControllerBase
    {
        private readonly IProfileImageGetterService _profileImageGetterService;
        private readonly IContactInformationVisibilityGetterService _contactInformationVisibilityGetterService;
        private readonly IContactInformationVisibilityEditorService _contactInformationVisibilityEditorService;
        private readonly ISecuritySettingsGetterService _securitySettingsGetterService;
        private readonly ISecuritySettingsEditorService _securitySettingsEditorService;

        public SettingsController(IProfileImageGetterService profileImageGetterService, IContactInformationVisibilityGetterService contactInformationVisibilityGetterService, IContactInformationVisibilityEditorService contactInformationVisibilityEditorService, ISecuritySettingsGetterService securitySettingsGetterService, ISecuritySettingsEditorService securitySettingsEditorService)
        {
            _profileImageGetterService = profileImageGetterService;
            _contactInformationVisibilityGetterService = contactInformationVisibilityGetterService;
            _contactInformationVisibilityEditorService = contactInformationVisibilityEditorService;
            _securitySettingsGetterService = securitySettingsGetterService;
            _securitySettingsEditorService = securitySettingsEditorService;
        }

        [HttpGet("get-profile-image")]
        public async Task<ActionResult<ProfileImagePathToResponse>> GetProfileImage()
        {
            var profileImagePath = await _profileImageGetterService.Get();

            return Ok(profileImagePath);
        }

        [HttpGet("get-contact-information-visibility")]
        public async Task<ActionResult<ContactInformationVisibilityToResponse>> GetContactInformationVisibility()
        {
            var contactInformationVisibility = await _contactInformationVisibilityGetterService.Get();

            return Ok(contactInformationVisibility);
        }

        [HttpPost("edit-contact-information-visibility")]
        public async Task<IActionResult> EditContactInformationVisibility(EditContactInformationVisibilityDTO dto)
        {
            var contactInformationVisibility = await _contactInformationVisibilityEditorService.Edit(dto.ContactInformationVisibilityType);

            return Ok(contactInformationVisibility);
        }

        [HttpGet("is-user-has-login-notification-enabled")]
        public async Task<ActionResult<IsLoginNotificationEnabledDTO>> IsUserHasLoginNotificationEnabled()
        {
            var isLoginNotificationEnabledInfo = await _securitySettingsGetterService.GetIsLoginNotificationEnabled();

            return Ok(isLoginNotificationEnabledInfo);
        }

        [HttpPost("edit-user-login-notification-enabled")]
        public async Task<ActionResult<IsLoginNotificationEnabledDTO>> EditUserLoginNotificationEnabled(IsLoginNotificationEnabledDTO dto)
        {
            var isLoginNotificationEnabledInfo = await _securitySettingsEditorService.EditIsLoginNotificationEnabled(dto);

            return Ok(isLoginNotificationEnabledInfo);
        }

        [HttpPost("edit-security-settings")]
        public async Task<ActionResult<IsLoginNotificationEnabledDTO>> EditSecuritySettings(EditSecuritySettingsDTO dto)
        {
            var isLoginNotificationEnabledInfo = await _securitySettingsEditorService.EditSecuritySettings(dto);

            return Ok(isLoginNotificationEnabledInfo);
        }
    }
}
