namespace FindACoach.Core.ServiceContracts.MyProfile.Projects
{
    /// <summary>
    /// Represents the service for deleting projects.
    /// </summary>
    public interface IProjectsRemoverService
    {
        /// <summary>
        /// Deletes project from the system.
        /// </summary>
        /// <param name="projectId">Project id which will be deleted.</param>
        /// <returns></returns>
        Task DeleteProject(string projectId);
    }
}
