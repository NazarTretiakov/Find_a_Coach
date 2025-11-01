using FindACoach.Core.Domain.Entities.Network;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.Enums;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FindACoach.Infrastructure.Repositories
{
    public class ConnectionsRepository: IConnectionsRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public ConnectionsRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task AcceptConnectionRequest(string connectionId)
        {
            var connection = await _db.Connections.FirstOrDefaultAsync(c => c.Id == Guid.Parse(connectionId));

            if (connection == null)
            {
                throw new ArgumentException("Connection not found.");
            }

            connection.Status = ConnectionStatus.Accepted;
            await _db.SaveChangesAsync();

            // TODO: Send notification to the user which was sending request about acceptance
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

            // TODO: Send notification to the user which was sending request about rejection
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
                    DateOfCreation = c.DateOfCreation
                })
                .FirstOrDefaultAsync();

            return connection;
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
    }
}
