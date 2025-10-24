using FindACoach.Core.DTO.MyProfile.Recommendations;
using FindACoach.Core.ServiceContracts.MyProfile.Recommendations;
using Microsoft.AspNetCore.Mvc;

namespace FindACoach.API.Controllers.MyProfile
{
    public class RecommendationsController: CustomControllerBase
    {
        private readonly IRecommendationsAdderService _recommendationsAdderService;
        private readonly IRecommendationsGetterService _recommendationsGetterService;
        private readonly IRecommendationsRemoverService _recommendationsRemoverService;

        public RecommendationsController(IRecommendationsAdderService recommendationsAdderService, IRecommendationsGetterService recommendationsGetterService, IRecommendationsRemoverService recommendationsRemoverService)
        {
            _recommendationsAdderService = recommendationsAdderService;
            _recommendationsGetterService = recommendationsGetterService;
            _recommendationsRemoverService = recommendationsRemoverService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddRecommendation([FromBody] AddRecommendationDTO dto)
        {
            await _recommendationsAdderService.AddRecommendation(dto);

            return Ok();
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<RecommendationToResponse>>> GetAllRecommendations(string userId)
        {
            var recommendations = await _recommendationsGetterService.GetAllRecommendations(userId);

            return Ok(recommendations);
        }

        [HttpGet("get-last-two")]
        public async Task<ActionResult<List<RecommendationToResponse>>> GetLastTwoRecommendations(string userId)
        {
            var recommendations = await _recommendationsGetterService.GetLastTwoRecommendations(userId);

            return Ok(recommendations);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteRecommendation([FromBody] DeleteRecommendationDTO dto)
        {
            await _recommendationsRemoverService.DeleteRecommendation(dto.RecommendationId);

            return Ok();
        }
    }
}