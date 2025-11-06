using FindACoach.Core.DTO.Network;

namespace FindACoach.Core.ServiceContracts.Network
{
    /// <summary>
    /// Represents service for retrieving notifications from system.
    /// </summary>
    public interface INotificationsGetterService
    {
        /// <summary>
        /// Retrieves all notifications of user.
        /// </summary>
        /// <param name="userId">User id which notifications will be retrieved.</param>
        /// <returns></returns>
        Task<List<NotificationToResponse>> GetAllUserNotifications(string userId, int page, int pageSize);
    }
}
