using FindACoach.Core.DTO.MyProfile.Projects;

namespace FindACoach.Core.ServiceContracts.MyProfile.Projects
{
    /// <summary>
    /// Represents the service for editing projects.
    /// </summary>
    public interface IProjectsEditorService
    {
        /// <summary>
        /// Edits existing project of user.
        /// </summary>
        /// <param name="dto">Data Transfer Object with data of project to edit.</param>
        /// <returns></returns>
        Task EditProject(EditProjectDTO dto);
    }
}
