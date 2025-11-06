using FindACoach.Core.Domain.Entities.Network;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.Enums;
using FindACoach.Core.ServiceContracts.Network;

namespace FindACoach.Core.Services.Network
{
    public class NotificationsAdderService : INotificationsAdderService
    {
        private readonly INotificationsRepository _notificationsRepository;

        public NotificationsAdderService(INotificationsRepository notificationsRepository)
        {
            _notificationsRepository = notificationsRepository;
        }

        public async Task AddNotification(string userId, string content, string notifiedObjectId, NotificationType notificationType)
        {
            Notification notification = new Notification()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                Content = content,
                Type = notificationType,
                DateOfCreation = DateTime.Now,
                NotifiedObjectId = Guid.Parse(notifiedObjectId)
            };

            await _notificationsRepository.Add(notification);
        }
    }
}
