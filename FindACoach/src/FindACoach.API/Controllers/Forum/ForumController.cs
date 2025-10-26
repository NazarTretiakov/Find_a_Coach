using FindACoach.Core.DTO.MyProfile.Activities;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using Microsoft.AspNetCore.Mvc;

namespace FindACoach.API.Controllers.Forum
{
    public class ForumController: CustomControllerBase
    {
        private readonly IActivitiesGetterService _activitiesGetterService;

        public ForumController(IActivitiesGetterService activitiesGetterService)
        {
            _activitiesGetterService = activitiesGetterService;
        }

        [HttpGet("get-recommended-activities")]
        public async Task<ActionResult<List<ActivityForActivitiesListToResponse>>> GetRecommendedActivities(string userId, int page = 1, int pageSize = 7)
        {
            List<ActivityForActivitiesListToResponse> activities = await _activitiesGetterService.GetRecommendedActivitiesPaged(userId, page, pageSize);

            return Ok(activities);
        }

        [HttpGet("get-filtered-activities")]
        public async Task<ActionResult<List<ActivityForActivitiesListToResponse>>> GetFilteredActivities(string searchString, int page = 1, int pageSize = 7)
        {
            List<ActivityForActivitiesListToResponse> activities = await _activitiesGetterService.GetFilteredRecommendedActivitiesPaged(page, pageSize, searchString);

            return Ok(activities);
        }
    }
}
