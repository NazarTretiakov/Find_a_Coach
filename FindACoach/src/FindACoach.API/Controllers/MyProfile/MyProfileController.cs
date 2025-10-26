using FindACoach.Core.DTO.MyProfile;
using FindACoach.Core.DTO.MyProfile.Activities;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using FindACoach.Core.ServiceContracts.MyProfile;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindACoach.API.Controllers.MyProfile
{
    public class MyProfileController : CustomControllerBase
    {
        private readonly IEditPersonalInformationService _editPersonalInformationService;
        private readonly IGetPersonalInformationService _getPersonalInformationService;
        private readonly IGetPersonalAndContactInformationService _getPersonalAndContactInformationService;
        private readonly IContactInformationGetterService _contactInformationGetterService;
        private readonly IEditAboutMeService _editAboutMeService;
        private readonly IGetAboutMeService _getAboutMeService;
        private readonly IActivitiesGetterService _activitiesGetterService;
        private readonly IIsProfileSectionsCompletedService _isProfileSectionsCompletedService;

        public MyProfileController(IEditPersonalInformationService editPersonalInformationService, IGetPersonalInformationService getPersonalInformationService, IGetPersonalAndContactInformationService getPersonalAndContactInformationService, IContactInformationGetterService contactInformationGetterService, IGetAboutMeService getAboutMeService, IEditAboutMeService editAboutMeService, IActivitiesGetterService activitiesGetterService, IIsProfileSectionsCompletedService isProfileSectionsCompletedService)
        {
            _editPersonalInformationService = editPersonalInformationService;
            _getPersonalInformationService = getPersonalInformationService;
            _getPersonalAndContactInformationService = getPersonalAndContactInformationService;
            _contactInformationGetterService = contactInformationGetterService;
            _getAboutMeService = getAboutMeService;
            _editAboutMeService = editAboutMeService;
            _activitiesGetterService = activitiesGetterService;
            _isProfileSectionsCompletedService = isProfileSectionsCompletedService;
        }

        [HttpPost("edit-personal-information")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditPersonalInformation([FromForm] EditPersonalInformationDTO dto)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(new { errorMessage = errorMessage });
            }

            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _editPersonalInformationService.EditPersonalInformation(userId, dto);

            return Ok();
        }

        [HttpGet("get-personal-information")]
        public async Task<ActionResult<PersonalInformationToResponse>> GetPersonalInformation(string userId)
        {
            PersonalInformationToResponse personalInformation = await _getPersonalInformationService.GetPersonalInformation(userId);

            return Ok(personalInformation);
        }

        [HttpGet("get-personal-and-contact-information")]
        public async Task<ActionResult<PersonalAndContactInformationToResponse>> GetPersonalAndContactInformation()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            PersonalAndContactInformationToResponse personalInformation = await _getPersonalAndContactInformationService.GetPersonalAndContactInformation(userId);

            return Ok(personalInformation);
        }

        [HttpPost("edit-about-me")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditAboutMe([FromForm] AboutMeDTO dto)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(new { errorMessage = errorMessage });
            }

            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _editAboutMeService.EditAboutMe(userId, dto);

            return Ok();
        }

        [HttpGet("get-contact-information")]
        public async Task<ActionResult<ContactInformationToResponse>> GetContactInformation(string userId)
        {
            var contactInformation = await _contactInformationGetterService.GetContactInformation(userId);

            return Ok(contactInformation);
        }

        [HttpGet("get-about-me")]
        public async Task<ActionResult<AboutMeDTO>> GetAboutMe(string userId)
        {
            AboutMeDTO aboutMe = await _getAboutMeService.GetAboutMe(userId);

            return Ok(aboutMe);
        }

        [HttpGet("get-last-two-activities")]
        public async Task<ActionResult<List<ActivityCardToResponse>>> GetLastTwoActivities(string userId)
        {
            List<ActivityCardToResponse> activities = await _activitiesGetterService.GetLastTwoActivities(userId);

            return Ok(activities);
        }

        [HttpGet("get-activities-list")]
        public async Task<ActionResult<List<ActivityForActivitiesListToResponse>>> GetActivitiesList(string userId, int page = 1, int pageSize = 7)
        {
            List<ActivityForActivitiesListToResponse> activities = await _activitiesGetterService.GetActivitiesPaged(userId, page, pageSize);

            return Ok(activities);
        }

        [HttpGet("get-filtered-activities-list")]
        public async Task<ActionResult<List<ActivityForActivitiesListToResponse>>> GetFilteredActivitiesList(string userId, string searchString, int page = 1, int pageSize = 7)
        {
            List<ActivityForActivitiesListToResponse> activities = await _activitiesGetterService.GetFilteredActivitiesPaged(userId, page, pageSize, searchString);

            return Ok(activities);
        }

        [HttpGet("is-profile-sections-completed")]
        public async Task<ActionResult<IsProfileSectionsCompletedToResponse>> IsProfileSectionsCompleted(string userId)
        {
            var isProfileSectionsCompletedInfo = await _isProfileSectionsCompletedService.IsProfileSectionsCompleted(userId);

            return Ok(isProfileSectionsCompletedInfo);
        }
    }
}
