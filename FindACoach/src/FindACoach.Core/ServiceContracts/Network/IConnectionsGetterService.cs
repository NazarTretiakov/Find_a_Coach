using FindACoach.Core.DTO.Network;

namespace FindACoach.Core.ServiceContracts.Network
{
    /// <summary>
    /// Represents a service for retrieving connections from the system.
    /// </summary>
    public interface IConnectionsGetterService
    {
        /// <summary>
        /// Retrieves a connection request based on the provided DTO.
        /// </summary>
        /// <returns></returns>
        Task<ConnectionRequestToResponse> Get(ConnectionRequestIdDTO dto);

        /// <summary>
        /// Retrives all user connections from system.
        /// </summary>
        /// <param name="userId">User id which connections will be retrieved.</param>
        /// <returns></returns>
        Task<List<ConnectionToResponse>> GetAllUserConnections(string userId, int page, int pageSize);
    }
}
