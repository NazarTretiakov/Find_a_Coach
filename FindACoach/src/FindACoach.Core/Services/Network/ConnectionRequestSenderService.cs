using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.ServiceContracts.Network;

namespace FindACoach.Core.Services.Network
{
    public class ConnectionRequestSenderService : IConnectionRequestSenderService
    {
        private readonly IConnectionsRepository _connectionsRepository;
        private readonly IIsUsersConnectedService _isUsersConnectedService;

        public ConnectionRequestSenderService(IConnectionsRepository connectionsRepository, IIsUsersConnectedService isUsersConnectedService)
        {
            _connectionsRepository = connectionsRepository;
            _isUsersConnectedService = isUsersConnectedService;
        }

        public async Task SendConnectionRequest(ConnectionRequestDTO dto)
        {
            var isUsersConnectedInfo = await _isUsersConnectedService.IsUsersConnected(dto.UserId, dto.ConnectedUserId);

            if (isUsersConnectedInfo.IsUsersConnected)
            {
                throw new InvalidOperationException("Users are already connected.");
            }

            await _connectionsRepository.AddConnectionRequest(dto);

            // TODO: Implement notification logic here (send in-app notification (also email if users picked that option in settings))
        }
    }
}
