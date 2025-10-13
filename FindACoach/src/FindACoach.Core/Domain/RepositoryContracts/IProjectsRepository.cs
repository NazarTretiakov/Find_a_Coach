using FindACoach.Core.DTO.MyProfile.Projects;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Project entity.
    /// </summary>
    public interface IProjectsRepository
    {
        /// <summary>
        /// Adds project of the user.
        /// </summary>
        /// <param name="userId">User Id which project will be added.</param>
        /// <param name="dto">Data Transfer Object with data to add project.</param>
        /// <returns></returns>
        Task AddProject(string userId, AddProjectDTO dto);

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

        /// <summary>
        /// Edits existing project.
        /// </summary>
        /// <param name="dto">Data Transfer Object with data to edit project.</param>
        /// <param name="editorId">Id of user which edits the project (id of active user).</param>
        /// <returns></returns>
        Task EditProject(EditProjectDTO dto, string editorId);


        /// <summary>
        /// Deletes project from the system.
        /// </summary>
        /// <param name="projectId">Project id which will be deleted.</param>
        /// <param name="activeUserId">Id of user which wants to delete project (id of active user).</param>
        /// <returns></returns>
        Task DeleteProject(string projectId, string activeUserId);
    }
}
