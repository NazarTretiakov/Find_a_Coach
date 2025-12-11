using FindACoach.Core.DTO.Admin;
using FindACoach.Core.DTO.MyProfile.Activities;
using FindACoach.Core.ServiceContracts.Admin;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using FindACoach.Core.ServiceContracts.Network;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindACoach.API.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminController: CustomControllerBase
    {
        private readonly IUsersGetterService _usersGetterService;
        private readonly IToggleBlockOfUserService _toggleBlockOfUserService;
        private readonly IActivitiesGetterService _activitiesGetterService;

        public AdminController(IUsersGetterService usersGetterService, IToggleBlockOfUserService toggleBlockOfUserService, IActivitiesGetterService activitiesGetterService)
        {
            _usersGetterService = usersGetterService;
            _toggleBlockOfUserService = toggleBlockOfUserService;
            _activitiesGetterService = activitiesGetterService;
        }

        [HttpGet("get-all-users")]
        public async Task<ActionResult<List<UserToResponse>>> GetAllUsers(int page, int pageSize)
        {
            var users = await _usersGetterService.GetAllUsers(page, pageSize);

            return Ok(users);
        }

        [HttpGet("get-filtered-users")]
        public async Task<ActionResult<List<UserToResponse>>> GetFilteredUsers(string searchString, int page, int pageSize)
        {
            var users = await _usersGetterService.GetFilteredUsers(searchString, page, pageSize);

            return Ok(users);
        }

        [HttpPost("toggle-block-of-user")]
        public async Task<ActionResult<bool>> ToggleBlockOfUser(UserIdDTO dto)
        {
            bool isBlocked = await _toggleBlockOfUserService.Toggle(dto.UserId);

            return Ok(isBlocked);
        }

        [HttpGet("get-all-activities")]
        public async Task<ActionResult<List<ActivityForActivitiesListToResponse>>> GetAllActivities(int page, int pageSize)
        {
            var activities = await _activitiesGetterService.GetAllActivities(page, pageSize);

            return Ok(activities);
        }
    }
}
