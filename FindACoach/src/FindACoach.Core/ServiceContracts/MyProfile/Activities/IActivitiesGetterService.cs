using FindACoach.Core.DTO.MyProfile.Activities;

namespace FindACoach.Core.ServiceContracts.MyProfile.Activities
{
    /// <summary>
    /// Represents the service for retriving activities from system
    /// </summary>
    public interface IActivitiesGetterService
    {
        /// <summary>
        /// Retrieves two last activities of user.
        /// </summary>
        /// <param name="userId">User id which activities will be retrieved.</param>
        /// <returns>ActivityCardToResponse object with data of activity.</returns>
        Task<List<ActivityCardToResponse>> GetLastTwoActivities(string userId);

        /// <summary>
        /// Retrieves all activities of user.
        /// </summary>
        /// <param name="userId">User id which activities will be retrieved.</param>
        /// <param name="page">Number of page that will be loaded</param>
        /// <param name="pageSize">Size of page which will be loaded.</param>
        /// <returns>ActivityForActivitiesListToResponse</returns>
        Task<List<ActivityForActivitiesListToResponse>> GetActivitiesPaged(string userId, int page, int pageSize);
    }
}
