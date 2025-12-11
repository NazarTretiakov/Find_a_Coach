using FindACoach.Core.Domain.Entities.Network;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.Enums;
using FindACoach.Core.ServiceContracts.Network;
using FindACoach.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FindACoach.Infrastructure.Repositories
{
    public class ConnectionsRepository: IConnectionsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly INotificationsAdderService _notificationsAdderService;
        private readonly UserManager<User> _userManager;

        public ConnectionsRepository(ApplicationDbContext db, IConfiguration configuration, INotificationsAdderService notificationsAdderService, UserManager<User> userManager)
        {
            _db = db;
            _configuration = configuration;
            _notificationsAdderService = notificationsAdderService;
            _userManager = userManager;
        }

        public async Task AcceptConnectionRequest(string connectionId)
        {
            var connection = await _db.Connections
                .Where(c => c.Id == Guid.Parse(connectionId))
                .Include(c => c.ConnectedUser)
                .FirstOrDefaultAsync();

            if (connection == null)
            {
                throw new ArgumentException("Connection not found.");
            }

            connection.Status = ConnectionStatus.Accepted;
            await _db.SaveChangesAsync();

            await _notificationsAdderService.AddNotification(connection.UserId.ToString(),
                $"Your connection request to {connection.ConnectedUser.FirstName} has been accepted.",
                connection.Id.ToString(),
                NotificationType.ConnectionRequestAcceptance);
        }

        public async Task AddConnectionRequest(ConnectionRequestDTO dto)
        {
            if (dto.UserId == dto.ConnectedUserId)
            {
                throw new InvalidOperationException("You cannot send connection request to yourself.");
            }

            var connection = new Connection
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(dto.UserId),
                ConnectedUserId = Guid.Parse(dto.ConnectedUserId),
                Message = dto.Message,
                Status = ConnectionStatus.Pending,
                DateOfCreation = DateTime.Now
            };

            await _db.Connections.AddAsync(connection);

            await _db.SaveChangesAsync();

            User userWhichWantsToConnect = await _userManager.FindByIdAsync(connection.UserId.ToString());

            await _notificationsAdderService.AddNotification(connection.ConnectedUserId.ToString(),
                $"You have unread connection request from {userWhichWantsToConnect.FirstName}.",
                connection.Id.ToString(),
                NotificationType.ConnectionRequest);
        }

        public async Task DeclineConnectionRequest(string connectionId)
        {
            var connection = await _db.Connections.FirstOrDefaultAsync(c => c.Id == Guid.Parse(connectionId));

            if (connection == null)
            {
                throw new ArgumentException("Connection not found.");
            }

            connection.Status = ConnectionStatus.Rejected;
            await _db.SaveChangesAsync();

            await _notificationsAdderService.AddNotification(connection.UserId.ToString(), 
                $"Your connection request to {connection.ConnectedUser.FirstName} has been declined.",
                connection.Id.ToString(), 
                NotificationType.ConnectionRequestRejection);
        }

        public async Task<List<ConnectionToResponse>> GetAllUserConnections(string userId, int page, int pageSize)
        {
            var connections = await _db.Connections
                .Where(c => c.UserId == Guid.Parse(userId) || c.ConnectedUserId == Guid.Parse(userId))
                .OrderByDescending(n => n.DateOfCreation)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(c => c.User)
                .Include(c => c.ConnectedUser)
                .ToListAsync();

            var connectionsToResponse = new List<ConnectionToResponse>();

            var serverUrl = _configuration.GetValue<string>("ServerUrl");

            foreach (var connection in connections)
            {
                if (connection.Status == ConnectionStatus.Rejected || connection.Status == ConnectionStatus.Pending)
                {
                    continue;
                }
                if (connection.UserId == Guid.Parse(userId))
                {
                    connectionsToResponse.Add(new ConnectionToResponse()
                    {
                        ConnectedUserId = connection.ConnectedUserId,
                        FirstName = connection.ConnectedUser.FirstName,
                        LastName = connection.ConnectedUser.LastName,
                        Headline = connection.ConnectedUser.Headline,
                        ImagePath = $"{serverUrl}/Images/UserProfiles/{connection.ConnectedUser.ImagePath}",
                    });
                }
                else if (connection.ConnectedUserId  == Guid.Parse(userId))
                {
                    connectionsToResponse.Add(new ConnectionToResponse()
                    {
                        ConnectedUserId = connection.UserId,
                        FirstName = connection.User.FirstName,
                        LastName = connection.User.LastName,
                        Headline = connection.User.Headline,
                        ImagePath = $"{serverUrl}/Images/UserProfiles/{connection.User.ImagePath}",
                    });
                }
            }

            return connectionsToResponse;
        }

        public async Task<ConnectionRequestToResponse> GetConnection(string connectionId)
        {
            var serverUrl = _configuration.GetValue<string>("ServerUrl");

            var connection = await _db.Connections
                .Where(c => c.Id == Guid.Parse(connectionId))
                .Select(c => new ConnectionRequestToResponse()
                {
                    ConnectionId = c.Id,
                    UserId = c.User.Id,
                    UserFirstName = c.User.FirstName,
                    UserLastName = c.User.LastName,
                    UserImagePath = $"{serverUrl}/Images/UserProfiles/{c.User.ImagePath}",
                    Message = c.Message,
                    DateOfCreation = c.DateOfCreation,
                    Status = c.Status.ToString()
                })
                .FirstOrDefaultAsync();

            return connection;
        }

        public async Task<NetworkOverviewInfoToResponse> GetNetworkOverview(string userId)
        {
            var userConnections = await _db.Connections
                .Where(c => c.UserId == Guid.Parse(userId) || c.ConnectedUserId == Guid.Parse(userId))
                .Include(c => c.User)
                .Include(c => c.ConnectedUser)
                .ToListAsync();

            var connectionRequestsSent = await _db.Connections
                .Where(c => c.UserId == Guid.Parse(userId) && c.Status == ConnectionStatus.Pending)
                .ToListAsync();
                

            NetworkOverviewInfoToResponse networkOverview = new NetworkOverviewInfoToResponse()
            {
                NumberOfConnections = userConnections
                    .Where(c => c.Status == ConnectionStatus.Accepted)
                    .ToList()
                    .Count,
                NumberOfConnectionRequestsSent = connectionRequestsSent.Count
            };

            return networkOverview;
        }

        public async Task<IsUsersConnectedInfoToResponse> IsUsersConnected(string userId, string connectedUserId)
        {
            var connection = await _db.Connections.FirstOrDefaultAsync(c => c.UserId == Guid.Parse(userId) && c.ConnectedUserId == Guid.Parse(connectedUserId) ||
                                                                            c.UserId == Guid.Parse(connectedUserId) && c.ConnectedUserId == Guid.Parse(userId));

            IsUsersConnectedInfoToResponse isUsersConnectedInfo;

            if (connection == null)
            {
                isUsersConnectedInfo = new IsUsersConnectedInfoToResponse()
                {
                    IsUsersConnected = false,
                    Status = null
                };
            }
            else
            {
                isUsersConnectedInfo = new IsUsersConnectedInfoToResponse
                {
                    IsUsersConnected = connection.Status == ConnectionStatus.Accepted,
                    Status = connection.Status.ToString()
                };
            }

            return isUsersConnectedInfo;
        }

        public async Task RemoveConnection(RemoveConnectionDTO dto)
        {
            var connection = await _db.Connections.FirstOrDefaultAsync(c => c.UserId == Guid.Parse(dto.UserId) && c.ConnectedUserId == Guid.Parse(dto.ConnectedUserId) ||
                                                            c.UserId == Guid.Parse(dto.ConnectedUserId) && c.ConnectedUserId == Guid.Parse(dto.UserId));


            _db.Connections.Remove(connection);


            List<Notification> notificationAboutConnection = await _db.Notifications
                .Where(n => n.NotifiedObjectId == connection.Id)
                .ToListAsync();

            _db.Notifications.RemoveRange(notificationAboutConnection);


            await _db.SaveChangesAsync();
        }
    }
}
