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
    }
}
