using FindACoach.Core.DTO.MyProfile.Projects;
using FindACoach.Core.ServiceContracts.MyProfile.Projects;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindACoach.API.Controllers.MyProfile
{
    public class ProjectsController : CustomControllerBase
    {
        private readonly IProjectsAdderService _projectsAdderService;
        private readonly IProjectsGetterService _projectsGetterService;

        public ProjectsController(IProjectsAdderService projectsAdderService, IProjectsGetterService projectsGetterService)
        {
            _projectsAdderService = projectsAdderService;
            _projectsGetterService = projectsGetterService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProject([FromBody] AddProjectDTO dto)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _projectsAdderService.AddProject(userId, dto);

            return Ok();
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<ProjectToResponse>>> GetAllProjects(string userId)
        {
            var projects = await _projectsGetterService.GetAllProjects(userId);

            return Ok(projects);
        }

        [HttpGet("get-last-two")]
        public async Task<ActionResult<List<ProjectToResponse>>> GetLastTwoProjects(string userId)
        {
            var projects = await _projectsGetterService.GetLastTwoProjects(userId);

            return Ok(projects);
        }

        [HttpGet("get")]
        public async Task<ActionResult<ProjectToResponse>> GetProject(string projectId)
        {
            var project = await _projectsGetterService.GetProject(projectId);

            return Ok(project);
        }
    }
}
