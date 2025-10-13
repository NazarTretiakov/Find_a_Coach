using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Projects;
using FindACoach.Core.ServiceContracts.MyProfile.Projects;

namespace FindACoach.Core.Services.MyProfile.Education
{
    public class ProjectsGetterService : IProjectsGetterService
    {
        private readonly IProjectsRepository _projectsRepository;

        public ProjectsGetterService(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }
        public async Task<List<ProjectToResponse>> GetAllProjects(string userId)
        {
            return await _projectsRepository.GetAllProjects(userId);
        }

        public async Task<List<ProjectToResponse>> GetLastTwoProjects(string userId)
        {
            return await _projectsRepository.GetLastTwoProjects(userId);
        }

        public async Task<ProjectToResponse> GetProject(string projectId)
        {
            return await _projectsRepository.GetProject(projectId);
        }
    }
}
