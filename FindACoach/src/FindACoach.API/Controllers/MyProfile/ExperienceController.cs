using FindACoach.Core.DTO.MyProfile.Experience;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using FindACoach.Core.ServiceContracts.MyProfile.Experience;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindACoach.API.Controllers.MyProfile
{
    public class ExperienceController : CustomControllerBase
    {
        private readonly IPositionsAdderService _positionsAdderService;
        private readonly IPositionsGetterService _positionsGetterService;
        private readonly IPositionsEditorService _positionsEditorService;
        private readonly IPositionsRemoverService _positionsRemoverService;

        public ExperienceController(IPositionsAdderService positionsAdderService, IPositionsGetterService positionsGetterService, IPositionsEditorService positionsEditorService, IPositionsRemoverService positionsRemoverService)
        {
            _positionsAdderService = positionsAdderService;
            _positionsGetterService = positionsGetterService;
            _positionsEditorService = positionsEditorService;
            _positionsRemoverService = positionsRemoverService;
        }

        [HttpPost("add-position")]
        public async Task<IActionResult> AddPosition([FromBody] AddPositionDTO dto)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _positionsAdderService.AddPosition(userId, dto);

            return Ok();
        }

        [HttpGet("get-all-positions")]
        public async Task<ActionResult<List<PositionToResponse>>> GetAllPositions(string userId)
        {
            var positions = await _positionsGetterService.GetAllPositions(userId);

            return Ok(positions);
        }

        [HttpGet("get-last-two-positions")]
        public async Task<ActionResult<List<PositionToResponse>>> GetLastTwoPositions(string userId)
        {
            var positions = await _positionsGetterService.GetLastTwoPositions(userId);

            return Ok(positions);
        }

        [HttpGet("get-position")]
        public async Task<ActionResult<PositionToResponse>> GetPosition(string positionId)
        {
            var position = await _positionsGetterService.GetPosition(positionId);

            return Ok(position);
        }

        [HttpPost("edit-position")]
        public async Task<IActionResult> EditPosition([FromBody] EditPositionDTO dto)
        {
            await _positionsEditorService.EditPosition(dto);

            return Ok();
        }

        [HttpPost("delete-position")]
        public async Task<IActionResult> DeletePosition([FromBody] DeletePositionDTO dto)
        {
            await _positionsRemoverService.DeletePosition(dto.PositionId);

            return Ok();
        }
    }
}