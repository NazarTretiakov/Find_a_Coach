using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.ServiceContracts.Network;

namespace FindACoach.Core.Services.Network
{
    public class AcceptConnectionRequestService : IAcceptConnectionRequestService
    {
        private readonly IConnectionsRepository _connectionsRepository;

        public AcceptConnectionRequestService(IConnectionsRepository connectionsRepository)
        {
            _connectionsRepository = connectionsRepository;
        }

        public async Task AcceptConnectionRequest(ConnectionRequestIdDTO dto)
        {
            await _connectionsRepository.AcceptConnectionRequest(dto.ConnectionId);
        }
    }
}
