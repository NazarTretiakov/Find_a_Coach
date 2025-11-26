namespace FindACoach.Core.ServiceContracts.MyProfile
{
    /// <summary>
    /// Represents the service for check if the user is blocked.
    /// </summary>
    public interface IIsUserBlockedCheckerService
    {
        /// <summary>
        /// Checks if the user blocked.
        /// </summary>
        /// <param name="userId">User id which will be checked.</param>
        /// <returns></returns>
        Task<bool> IsUserBlocked(string userId);
    }
}
