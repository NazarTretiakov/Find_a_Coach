using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.ServiceContracts.Network;

namespace FindACoach.Core.Services.Network
{
    public class CheckUnreadNotificationsService : ICheckUnreadNotificationsService
    {
        private readonly INotificationsRepository _notificationsRepository;

        public CheckUnreadNotificationsService(INotificationsRepository notificationsRepository)
        {
            _notificationsRepository = notificationsRepository;
        }

        public async Task<bool> CheckUserUnreadNotifications(string userId)
        {
            return await _notificationsRepository.CheckUserUnreadNotifications(userId);
        }
    }
}
