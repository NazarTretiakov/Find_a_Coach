using FindACoach.Core.DTO.MyProfile.Projects;

namespace FindACoach.Core.ServiceContracts.MyProfile.Projects
{
    /// <summary>
    /// Represents the service for getting projects.
    /// </summary>
    public interface IProjectsGetterService
    {
        /// <summary>
        /// Retrieves two last projects of user.
        /// </summary>
        /// <param name="userId">User id which projects will be retrieved.</param>
        /// <returns></returns>
        Task<List<ProjectToResponse>> GetLastTwoProjects(string userId);

        /// <summary>
        /// Retrieves all projects of user.
        /// </summary>
        /// <param name="userId">User id which projects will be retrieved.</param>
        /// <returns></returns>
        Task<List<ProjectToResponse>> GetAllProjects(string userId);

        /// <summary>
        /// Retrieves project by id.
        /// </summary>
        /// <param name="projectId">Project id which will be retrieved.</param>
        /// <returns></returns>
        Task<ProjectToResponse> GetProject(string projectId);
    }
}
