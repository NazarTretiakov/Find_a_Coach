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
        private readonly INotificationsGetterService _notificationsGetterService;
        private readonly IConnectionsRemoverService _connectionsRemoverService;

        public NetworkController(IIsUsersConnectedService isUsersConnectedService, IConnectionRequestSenderService connectionRequestSenderService, IConnectionsGetterService connectionsGetterService, IAcceptConnectionRequestService acceptConnectionRequestService, IDeclineConnectionRequestService declineConnectionRequestService, INotificationsGetterService notificationsGetterService, IConnectionsRemoverService connectionsRemoverService)
        {
            _isUsersConnectedService = isUsersConnectedService;
            _connectionRequestSenderService = connectionRequestSenderService;
            _connectionsGetterService = connectionsGetterService;
            _acceptConnectionRequestService = acceptConnectionRequestService;
            _declineConnectionRequestService = declineConnectionRequestService;
            _notificationsGetterService = notificationsGetterService;
            _connectionsRemoverService = connectionsRemoverService;
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

        [HttpGet("get-connection-request")]
        public async Task<ActionResult<ConnectionRequestToResponse>> GetConnectionRequest([FromQuery] ConnectionRequestIdDTO dto)
        {
            ConnectionRequestToResponse connectionRequest = await _connectionsGetterService.Get(dto);

            return Ok(connectionRequest);
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

        [HttpGet("get-notifications")]
        public async Task<ActionResult<List<NotificationToResponse>>> GetNotifications(string userId, int page, int pageSize)
        {
            var notifications = await _notificationsGetterService.GetAllUserNotifications(userId, page, pageSize);

            return Ok(notifications);
        }

        [HttpPost("remove-connection")]
        public async Task<ActionResult<List<NotificationToResponse>>> RemoveConnection([FromBody] RemoveConnectionDTO dto)
        {
            await _connectionsRemoverService.RemoveConnection(dto);

            return Ok();
        }
    }
}
