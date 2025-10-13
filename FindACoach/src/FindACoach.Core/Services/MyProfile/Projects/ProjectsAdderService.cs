using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Projects;
using FindACoach.Core.ServiceContracts.MyProfile.Projects;

namespace FindACoach.Core.Services.MyProfile.Projects
{
    public class ProjectsAdderService : IProjectsAdderService
    {
        private readonly IProjectsRepository _projectsRepository;

        public ProjectsAdderService(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }

        public async Task AddProject(string userId, AddProjectDTO dto)
        {
            await _projectsRepository.AddProject(userId, dto);
        }
    }
}
