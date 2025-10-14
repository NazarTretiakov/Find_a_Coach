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
        private readonly IProjectsEditorService _projectsEditorService;
        private readonly IProjectsRemoverService _projectsRemoverService;

        public ProjectsController(IProjectsAdderService projectsAdderService, IProjectsGetterService projectsGetterService, IProjectsEditorService projectsEditorService, IProjectsRemoverService projectsRemoverService)
        {
            _projectsAdderService = projectsAdderService;
            _projectsGetterService = projectsGetterService;
            _projectsEditorService = projectsEditorService;
            _projectsRemoverService = projectsRemoverService;
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

        [HttpPost("edit")]
        public async Task<IActionResult> EditProject([FromBody] EditProjectDTO dto)
        {
            await _projectsEditorService.EditProject(dto);

            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteProject([FromBody] DeleteProjectDTO dto)
        {
            await _projectsRemoverService.DeleteProject(dto.ProjectId);

            return Ok();
        }
    }
}
