using FindACoach.Core.Domain.Entities.Network;
using FindACoach.Core.DTO.Network;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Notification entity.
    /// </summary>
    public interface INotificationsRepository
    {
        /// <summary>
        /// Retrieves all notifications of user.
        /// </summary>
        /// <param name="userId">User id which notifications will be retrieved.</param>
        /// <returns></returns>
        Task<List<NotificationToResponse>> GetAllUserNotifications(string userId, int page, int pageSize);

        /// <summary>
        /// Adds a notification for a user.
        /// </summary>
        /// <param name="notification">Notification object that will be added.</param>
        /// <returns></returns>
        Task Add(Notification notification);
    }
}
