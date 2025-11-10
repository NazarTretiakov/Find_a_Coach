using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.ServiceContracts.Network;

namespace FindACoach.Core.Services.Network
{
    public class ConnectionsGetterService : IConnectionsGetterService
    {
        private readonly IConnectionsRepository _connectionsRepository;

        public ConnectionsGetterService(IConnectionsRepository connectionsRepository)
        {
            _connectionsRepository = connectionsRepository;
        }

        public async Task<ConnectionRequestToResponse> Get(ConnectionRequestIdDTO dto)
        {
            return await _connectionsRepository.GetConnection(dto.ConnectionId);
        }

        public async Task<List<ConnectionToResponse>> GetAllUserConnections(string userId, int page, int pageSize)
        {
            return await _connectionsRepository.GetAllUserConnections(userId, page, pageSize);
        }

        public async Task<List<ConnectionToResponse>> GetFilteredConnections(string userId, string searchString, int page, int pageSize)
        {
            return await _connectionsRepository.GetFilteredConnections(userId, searchString, page, pageSize);
        }

        public async Task<List<ConnectionToResponse>> GetRecommendedConnections(string userId, int page, int pageSize)
        {
            return await _connectionsRepository.GetRecommendedConnections(userId, page, pageSize);
        }
    }
}
