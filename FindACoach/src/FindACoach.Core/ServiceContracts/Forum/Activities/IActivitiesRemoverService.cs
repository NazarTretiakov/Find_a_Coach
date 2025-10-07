namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents the service for deleting activities.
    /// </summary>
    public interface IActivitiesRemoverService
    {
        /// <summary>
        /// Deletes activity from the system.
        /// </summary>
        /// <param name="activityId">Activity id which will be deleted.</param>
        /// <param name="userId">User id which created the activity.</param>
        /// <returns></returns>
        Task DeleteActivity(string activityId, string userId);
    }
}
