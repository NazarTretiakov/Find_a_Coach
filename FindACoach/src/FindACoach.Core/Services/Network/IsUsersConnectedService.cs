using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.ServiceContracts.Network;

namespace FindACoach.Core.Services.Network
{
    public class IsUsersConnectedService : IIsUsersConnectedService
    {
        private readonly IConnectionsRepository _connectionsRepository;

        public IsUsersConnectedService(IConnectionsRepository connectionsRepository)
        {
            _connectionsRepository = connectionsRepository;
        }

        public async Task<IsUsersConnectedInfoToResponse> IsUsersConnected(string userId, string connectedUserId)
        {
            return await _connectionsRepository.IsUsersConnected(userId, connectedUserId);
        }
    }
}
