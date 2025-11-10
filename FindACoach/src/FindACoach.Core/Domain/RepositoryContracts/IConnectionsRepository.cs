using FindACoach.Core.DTO.Network;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Connections entity.
    /// </summary>
    public interface IConnectionsRepository
    {
        /// <summary>
        /// Checks if two users are connected.
        /// </summary>
        /// <param name="userId">User which sended the request to connect. (Basically the first user)</param>
        /// <param name="connectedUserId">User which received request to connect. (Basically the second user)</param>
        /// <returns>IsUsersConnectedInfoToResponse object with info about is two users are connected</returns>
        Task<IsUsersConnectedInfoToResponse> IsUsersConnected(string userId, string connectedUserId);

        /// <summary>
        /// Retrieves a connection request based on the provided id.
        /// <param name="connectionId">The Id of connection that will be retrieved.</param>"
        /// </summary>
        /// <returns></returns>
        Task<ConnectionRequestToResponse> GetConnection(string connectionId);

        /// <summary>
        /// Retrives all user connections from system.
        /// </summary>
        /// <param name="userId">User id which connections will be retrieved.</param>
        /// <returns></returns>
        Task<List<ConnectionToResponse>> GetAllUserConnections(string userId, int page, int pageSize);

        /// <summary>
        /// Adds a connection request based on the provided DTO.
        /// </summary>
        /// <returns></returns>
        Task AddConnectionRequest(ConnectionRequestDTO dto);

        /// <summary>
        /// Accepts a connection request based on the provided connection id.
        /// </summary>
        /// <param name="connectionId">The Id of connection that will be accepted.</param>"
        /// <returns></returns>
        Task AcceptConnectionRequest(string connectionId);

        /// <summary>
        /// Declines a connection request based on the provided connection id.
        /// <param name="connectionId">The Id of connection that will be declined.</param>"
        /// </summary>
        /// <returns></returns>
        Task DeclineConnectionRequest(string connectionId);

        /// <summary>
        /// Removes connection from the system.
        /// </summary>
        /// <param name="dto">Data Transfer Object with connection information to remove.</param>
        /// <returns></returns>
        Task RemoveConnection(RemoveConnectionDTO dto);

        /// <summary>
        /// Retrieves the information about network overview of user.
        /// </summary>
        /// <param name="userId">User id which information about network will be retrieved.</param>
        /// <returns></returns>
        Task<NetworkOverviewInfoToResponse> GetNetworkOverview(string userId);

        /// <summary>
        /// Retrieves recommended connections for user.
        /// </summary>
        /// <param name="userId">User id which recommended connections will be retrieved.</param>
        /// <returns></returns>
        Task<List<ConnectionToResponse>> GetRecommendedConnections(string userId, int page, int pageSize);

        /// <summary>
        /// Retrieves filtered connections
        /// </summary>
        /// <param name="searchString">Search string to filter the connections.</param>
        /// <returns></returns>
        Task<List<ConnectionToResponse>> GetFilteredConnections(string userId, string searchString, int page, int pageSize);
    }
}
