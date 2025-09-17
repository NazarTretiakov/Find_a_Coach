using FindACoach.Core.DTO;
using FindACoach.Core.ServiceContracts.MyProfile;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindACoach.API.Controllers
{
    public class MyProfileController : CustomControllerBase
    {
        private readonly IEditPersonalInformationService _editPersonalInformationService;

        public MyProfileController(IEditPersonalInformationService editPersonalInformationService)
        {
            _editPersonalInformationService = editPersonalInformationService;
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
    }
}
