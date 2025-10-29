using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.DTO.Forum;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Language entity.
    /// </summary>
    public interface IEventApplicationsRepository
    {

        /// <summary>
        /// Adds an application on search person panel of event.
        /// </summary>
        /// <param name="dto">The data transfer object that contains information about the application on search person panel that will be created.</param>
        /// <returns></returns>
        Task Add(EventApplication application);

        /// <summary>
        /// Retrieves all applications on particular search person panel of event.
        /// </summary>
        /// <param name="searchPersonPanelId">Search person panel id which application will be retrieved</param>
        /// <returns></returns>
        Task<List<EventApplicationToResponse>> GetApplications(string searchPersonPanelId);
    }
}
