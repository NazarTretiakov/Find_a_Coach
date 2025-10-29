using FindACoach.Core.DTO.Forum;

namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents the service for creating applications on search person panels of events.
    /// </summary>
    public interface IEventApplicationsCreatorService
    {
        /// <summary>
        /// Adds an application on search person panel of event.
        /// </summary>
        /// <param name="dto">The data transfer object that contains information about the application on search person panel that will be created.</param>
        /// <returns></returns>
        Task ApplyOnEvent(ApplyOnEventDTO dto);
    }
}
