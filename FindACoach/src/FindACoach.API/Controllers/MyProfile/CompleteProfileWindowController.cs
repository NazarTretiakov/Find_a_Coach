using FindACoach.Core.DTO.MyProfile;
using FindACoach.Core.ServiceContracts.CompleteProfileWindow;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindACoach.API.Controllers.MyProfile
{
    public class CompleteProfileWindowController : CustomControllerBase
    {
        private readonly IChangeStateService _changeStateService;
        private readonly IGetStateService _getStateService;

        public CompleteProfileWindowController(IChangeStateService changeStateService, IGetStateService getStateService)
        {
            _changeStateService = changeStateService;
            _getStateService = getStateService;
        }

        [HttpPost("change-state")]
        public async Task<IActionResult> ChangeState([FromBody] CompleteProfileWindowStateDTO dto)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(new { errorMessage = errorMessage });
            }

            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _changeStateService.ChangeState(userId, dto.IsVisible, dto.Title);

            return Ok();
        }

        [HttpPost("get-state")]
        public async Task<ActionResult<CompleteProfileWindowStateDTO>> GetState()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            CompleteProfileWindowStateDTO state = await _getStateService.GetState(userId);

            return Ok(state);
        }
    }
}