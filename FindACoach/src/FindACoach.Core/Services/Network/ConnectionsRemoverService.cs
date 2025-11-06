using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.ServiceContracts.Network;

namespace FindACoach.Core.Services.Network
{
    public class ConnectionsRemoverService : IConnectionsRemoverService
    {
        private readonly IConnectionsRepository _connectionsRepository;

        public ConnectionsRemoverService(IConnectionsRepository connectionsRepository)
        {
            _connectionsRepository = connectionsRepository;
        }

        public async Task RemoveConnection(RemoveConnectionDTO dto)
        {
            await _connectionsRepository.RemoveConnection(dto);
        }
    }
}
