using FindACoach.Core.Domain.Entities.Network;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.Enums;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FindACoach.Infrastructure.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;

        public NotificationsRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task Add(Notification notification)
        {
            await _db.Notifications.AddAsync(notification);

            await _db.SaveChangesAsync();
        }

        public async Task<List<NotificationToResponse>> GetAllUserNotifications(string userId, int page, int pageSize)
        {
            List<NotificationToResponse> notifications = await _db.Notifications
                .Where(n => n.UserId == Guid.Parse(userId))
                .OrderByDescending(n => n.DateOfCreation)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(n => new NotificationToResponse()
                {
                    NotificationId = n.Id,
                    Content = n.Content,
                    DateOfCreation = n.DateOfCreation,
                    NotifiedObjectId = n.NotifiedObjectId,
                    Type = n.Type.ToString(),
                    RelatedUserFirstName = "",
                    RelatedUserLastName = "",
                    RelatedUserImagePath = ""
                })
                .ToListAsync();

            foreach (var notification in notifications)
            {
                if (notification.Type == NotificationType.ConnectionRequest.ToString())
                {
                    var connection = await _db.Connections
                        .Where(c => c.Id == notification.NotifiedObjectId)
                        .Select(c => new
                        {
                            RelatedUserFirstName = c.User.FirstName,
                            RelatedUserLastName = c.User.LastName,
                            RelatedUserImagePath = c.User.ImagePath,
                        })
                        .FirstOrDefaultAsync();

                    if (connection != null)
                    {
                        notification.RelatedUserFirstName = connection.RelatedUserFirstName;
                        notification.RelatedUserLastName = connection.RelatedUserLastName;
                        var serverUrl = _configuration.GetValue<string>("ServerUrl");
                        notification.RelatedUserImagePath = $"{serverUrl}/Images/UserProfiles/{connection.RelatedUserImagePath}";
                    }
                }
                else if (notification.Type == NotificationType.ConnectionRequestAcceptance.ToString() || notification.Type == NotificationType.ConnectionRequestRejection.ToString())
                {
                    var connection = await _db.Connections
                        .Where(c => c.Id == notification.NotifiedObjectId)
                        .Select(c => new
                        {
                            RelatedUserFirstName = c.ConnectedUser.FirstName,
                            RelatedUserLastName = c.ConnectedUser.LastName,
                            RelatedUserImagePath = c.ConnectedUser.ImagePath,
                        })
                        .FirstOrDefaultAsync();
                    if (connection != null)
                    {
                        notification.RelatedUserFirstName = connection.RelatedUserFirstName;
                        notification.RelatedUserLastName = connection.RelatedUserLastName;
                        var serverUrl = _configuration.GetValue<string>("ServerUrl");
                        notification.RelatedUserImagePath = $"{serverUrl}/Images/UserProfiles/{connection.RelatedUserImagePath}";
                    }
                }
                else if (notification.Type == NotificationType.EventApplication.ToString())
                {
                    var message = await _db.EventApplications
                        .Where(m => m.Id == notification.NotifiedObjectId)
                        .Select(m => new
                        {
                            RelatedUserFirstName = m.User.FirstName,
                            RelatedUserLastName = m.User.LastName,
                            RelatedUserImagePath = m.User.ImagePath,
                            NotifiedObjectId = m.SearchPersonPanel.EventId
                        })
                        .FirstOrDefaultAsync();
                    if (message != null)
                    {
                        notification.NotifiedObjectId = message.NotifiedObjectId;
                        notification.RelatedUserFirstName = message.RelatedUserFirstName;
                        notification.RelatedUserLastName = message.RelatedUserLastName;
                        var serverUrl = _configuration.GetValue<string>("ServerUrl");
                        notification.RelatedUserImagePath = $"{serverUrl}/Images/UserProfiles/{message.RelatedUserImagePath}";
                    }
                }
                else if (notification.Type == NotificationType.QAAnswer.ToString())
                {
                    var message = await _db.QAAnswers
                        .Where(m => m.Id == notification.NotifiedObjectId)
                        .Select(m => new
                        {
                            RelatedUserFirstName = m.User.FirstName,
                            RelatedUserLastName = m.User.LastName,
                            RelatedUserImagePath = m.User.ImagePath,
                            NotifiedObjectId = m.QAId
                        })
                        .FirstOrDefaultAsync();
                    if (message != null)
                    {
                        notification.NotifiedObjectId = message.NotifiedObjectId;
                        notification.RelatedUserFirstName = message.RelatedUserFirstName;
                        notification.RelatedUserLastName = message.RelatedUserLastName;
                        var serverUrl = _configuration.GetValue<string>("ServerUrl");
                        notification.RelatedUserImagePath = $"{serverUrl}/Images/UserProfiles/{message.RelatedUserImagePath}";
                    }
                }
                else if (notification.Type == NotificationType.Recommendation.ToString())
                {
                    var message = await _db.Recommendations
                        .Where(m => m.Id == notification.NotifiedObjectId)
                        .Select(m => new
                        {
                            RelatedUserFirstName = m.RecommenderUser.FirstName,
                            RelatedUserLastName = m.RecommenderUser.LastName,
                            RelatedUserImagePath = m.RecommenderUser.ImagePath,
                        })
                        .FirstOrDefaultAsync();
                    if (message != null)
                    {
                        notification.RelatedUserFirstName = message.RelatedUserFirstName;
                        notification.RelatedUserLastName = message.RelatedUserLastName;
                        var serverUrl = _configuration.GetValue<string>("ServerUrl");
                        notification.RelatedUserImagePath = $"{serverUrl}/Images/UserProfiles/{message.RelatedUserImagePath}";
                    }
                }
            }

            return notifications;
        }
    }
}
