using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.ServiceContracts.Network;

namespace FindACoach.Core.Services.Network
{
    public class NetworkOverviewGetterService : INetworkOverviewGetterService
    {
        private readonly IConnectionsRepository _connectionsRepository;

        public NetworkOverviewGetterService(IConnectionsRepository connectionsRepository)
        {
            _connectionsRepository = connectionsRepository;
        }

        public async Task<NetworkOverviewInfoToResponse> GetNetworkOverview(string userId)
        {
            return await _connectionsRepository.GetNetworkOverview(userId);
        }
    }
}
