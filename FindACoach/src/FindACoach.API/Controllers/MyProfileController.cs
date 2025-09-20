using FindACoach.Core.DTO;
using FindACoach.Core.DTO.MyProfile;
using FindACoach.Core.ServiceContracts.MyProfile;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindACoach.API.Controllers
{
    public class MyProfileController : CustomControllerBase
    {
        private readonly IEditPersonalInformationService _editPersonalInformationService;
        private readonly IGetPersonalInformationService _getPersonalInformationService;
        private readonly IGetPersonalAndContactInformationService _getPersonalAndContactInformationService;

        public MyProfileController(IEditPersonalInformationService editPersonalInformationService, IGetPersonalInformationService getPersonalInformationService, IGetPersonalAndContactInformationService getPersonalAndContactInformationService)
        {
            _editPersonalInformationService = editPersonalInformationService;
            _getPersonalInformationService = getPersonalInformationService;
            _getPersonalAndContactInformationService = getPersonalAndContactInformationService;
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
        public async Task<ActionResult<PersonalInformationToResponse>> GetPersonalInformation()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

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
    }
}
