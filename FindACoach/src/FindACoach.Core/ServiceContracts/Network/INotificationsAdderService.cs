using FindACoach.Core.Enums;

namespace FindACoach.Core.ServiceContracts.Network
{
    /// <summary>
    /// Represents a service for adding notifications.
    /// </summary>
    public interface INotificationsAdderService
    {
        /// <summary>
        /// Adds a notification for a user.
        /// </summary>
        /// <param name="userId">User id which will be notified.</param>
        /// <param name="content">Content of notification</param>
        /// <param name="notifiedObjectId">Object id about which the user will be notified</param>
        /// <param name="notificationType">Type of notification</param>
        /// <returns></returns>
        Task AddNotification(string userId, string content, string notifiedObjectId, NotificationType notificationType);
    }
}
