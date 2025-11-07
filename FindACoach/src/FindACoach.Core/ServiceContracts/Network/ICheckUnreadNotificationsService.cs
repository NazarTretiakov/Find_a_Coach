namespace FindACoach.Core.ServiceContracts.Network
{
    /// <summary>
    /// Represents a service contract for checking if a user has unread notifications.
    /// </summary>
    public interface ICheckUnreadNotificationsService
    {
        /// <summary>
        /// Checks if the user with the specified Id has unread notifications.
        /// </summary>
        /// <param name="userId">The Id of the user to check for unread notifications.</param>
        /// <returns>True if user has unread notifications, otherwise false.</returns>
        Task<bool> CheckUserUnreadNotifications(string userId);
    }
}
