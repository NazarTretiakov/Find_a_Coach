using FindACoach.Core.DTO.Network;
using FindACoach.Core.ServiceContracts.Network;
using Microsoft.AspNetCore.Mvc;

namespace FindACoach.API.Controllers.Network
{
    public class NetworkController: CustomControllerBase
    {
        private readonly IIsUsersConnectedService _isUsersConnectedService;
        private readonly IConnectionRequestSenderService _connectionRequestSenderService;
        private readonly IConnectionsGetterService _connectionsGetterService;
        private readonly IAcceptConnectionRequestService _acceptConnectionRequestService;
        private readonly IDeclineConnectionRequestService _declineConnectionRequestService;

        public NetworkController(IIsUsersConnectedService isUsersConnectedService, IConnectionRequestSenderService connectionRequestSenderService, IConnectionsGetterService connectionsGetterService, IAcceptConnectionRequestService acceptConnectionRequestService, IDeclineConnectionRequestService declineConnectionRequestService)
        {
            _isUsersConnectedService = isUsersConnectedService;
            _connectionRequestSenderService = connectionRequestSenderService;
            _connectionsGetterService = connectionsGetterService;
            _acceptConnectionRequestService = acceptConnectionRequestService;
            _declineConnectionRequestService = declineConnectionRequestService;
        }

        [HttpGet("is-users-connected")]
        public async Task<ActionResult<List<IsUsersConnectedInfoToResponse>>> IsUsersConnected(string userId, string connectedUserId)
        {
            var isUsersConnectedInfo = await _isUsersConnectedService.IsUsersConnected(userId, connectedUserId);

            return Ok(isUsersConnectedInfo);
        }

        [HttpPost("send-connection-request")]
        public async Task<IActionResult> SendConnectionRequest([FromBody] ConnectionRequestDTO dto)
        {
            await _connectionRequestSenderService.SendConnectionRequest(dto);

            return Ok();
        }

        [HttpPost("get-connection-request")]
        public async Task<ActionResult<ConnectionRequestToResponse>> GetConnectionRequest([FromBody] ConnectionRequestIdDTO dto)
        {
            await _connectionsGetterService.Get(dto);

            return Ok();
        }

        [HttpPost("accept-connection-request")]
        public async Task<IActionResult> AcceptConnectionRequest([FromBody] ConnectionRequestIdDTO dto)
        {
            await _acceptConnectionRequestService.AcceptConnectionRequest(dto);
            return Ok();
        }

        [HttpPost("decline-connection-request")]
        public async Task<IActionResult> DeclineConnectionRequest([FromBody] ConnectionRequestIdDTO dto)
        {
            await _declineConnectionRequestService.DeclineConnectionRequest(dto);
            return Ok();
        }
    }
}
