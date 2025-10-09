using FindACoach.Core.DTO.MyProfile.Experience;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindACoach.API.Controllers.MyProfile
{
    public class ExperienceController : CustomControllerBase
    {
        private readonly IPositionsAdderService _positionsAdderService;

        public ExperienceController(IPositionsAdderService positionsAdderService)
        {
            _positionsAdderService = positionsAdderService;
        }

        [HttpPost("add-position")]
        public async Task<IActionResult> AddPosition([FromBody] AddPositionDTO dto)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _positionsAdderService.AddPosition(userId, dto);

            return Ok();
        }
    }
}
