namespace FindACoach.Core.ServiceContracts.Admin
{
    /// <summary>
    /// Represents the service for toggling block of user.
    /// </summary>
    public interface IToggleBlockOfUserService
    {
        /// <summary>
        /// Toggles block of user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> Toggle(string userId);
    }
}
