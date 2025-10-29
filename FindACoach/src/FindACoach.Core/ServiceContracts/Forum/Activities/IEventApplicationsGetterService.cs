using FindACoach.Core.DTO.Forum;

namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents the service for retrieving applications of event.
    /// </summary>
    public interface IEventApplicationsGetterService
    {
        /// <summary>
        /// Retrieves all applications on particular search person panel of event.
        /// </summary>
        /// <param name="searchPersonPanelId">Search person panel id which application will be retrieved</param>
        /// <returns></returns>
        Task<List<EventApplicationToResponse>> GetPanelApplications(string searchPersonPanelId);
    }
}
