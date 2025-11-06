using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.ServiceContracts.Network;

namespace FindACoach.Core.Services.Network
{
    public class NotificationsGetterService : INotificationsGetterService
    {
        private readonly INotificationsRepository _notificationsRepository;

        public NotificationsGetterService(INotificationsRepository notificationsRepository)
        {
            _notificationsRepository = notificationsRepository;
        }

        public async Task<List<NotificationToResponse>> GetAllUserNotifications(string userId, int page, int pageSize)
        {
            return await _notificationsRepository.GetAllUserNotifications(userId, page, pageSize);
        }
    }
}
