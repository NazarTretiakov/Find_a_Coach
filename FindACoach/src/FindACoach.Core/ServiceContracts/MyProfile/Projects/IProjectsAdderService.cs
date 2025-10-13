using FindACoach.Core.DTO.MyProfile.Projects;

namespace FindACoach.Core.ServiceContracts.MyProfile.Projects
{
    /// <summary>
    /// Represents the service for adding projects.
    /// </summary>
    public interface IProjectsAdderService
    {
        /// <summary>
        /// Adds project of the user.
        /// </summary>
        /// <param name="userId">User Id which project will be added.</param>
        /// <param name="dto">Data Transfer Object with data to add project.</param>
        /// <returns></returns>
        Task AddProject(string userId, AddProjectDTO dto);
    }
}
