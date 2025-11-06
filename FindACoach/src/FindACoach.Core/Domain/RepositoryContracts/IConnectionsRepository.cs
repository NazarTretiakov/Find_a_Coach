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
    }
}
