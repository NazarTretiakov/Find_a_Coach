using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Projects;
using FindACoach.Core.ServiceContracts.MyProfile.Projects;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FindACoach.Core.Services.MyProfile.Projects
{
    public class ProjectsEditorService : IProjectsEditorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProjectsRepository _projectsRepository;

        public ProjectsEditorService(IHttpContextAccessor httpContextAccessor, IProjectsRepository projectsRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _projectsRepository = projectsRepository;
        }

        public async Task EditProject(EditProjectDTO dto)
        {
            var principal = _httpContextAccessor.HttpContext?.User;
            if (principal == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            string? activeUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (activeUserId == null)
            {
                throw new UnauthorizedAccessException("Cannot resolve user id from claims");
            }

            await _projectsRepository.EditProject(dto, activeUserId);
        }
    }
}
