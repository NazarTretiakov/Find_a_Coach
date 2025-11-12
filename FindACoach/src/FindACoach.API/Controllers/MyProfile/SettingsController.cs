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

        public SettingsController(IProfileImageGetterService profileImageGetterService, IContactInformationVisibilityGetterService contactInformationVisibilityGetterService, IContactInformationVisibilityEditorService contactInformationVisibilityEditorService)
        {
            _profileImageGetterService = profileImageGetterService;
            _contactInformationVisibilityGetterService = contactInformationVisibilityGetterService;
            _contactInformationVisibilityEditorService = contactInformationVisibilityEditorService;
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
    }
}
