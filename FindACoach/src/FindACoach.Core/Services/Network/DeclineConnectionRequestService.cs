using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.ServiceContracts.Network;

namespace FindACoach.Core.Services.Network
{
    public class DeclineConnectionRequestService: IDeclineConnectionRequestService
    {
        private readonly IConnectionsRepository _connectionsRepository;

        public DeclineConnectionRequestService(IConnectionsRepository connectionsRepository)
        {
            _connectionsRepository = connectionsRepository;
        }

        async Task IDeclineConnectionRequestService.DeclineConnectionRequest(ConnectionRequestIdDTO dto)
        {
            await _connectionsRepository.DeclineConnectionRequest(dto.ConnectionId);
        }
    }
}
