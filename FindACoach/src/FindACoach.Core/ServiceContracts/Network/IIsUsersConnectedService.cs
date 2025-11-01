using FindACoach.Core.DTO.Network;

namespace FindACoach.Core.ServiceContracts.Network
{
    /// <summary>
    /// Represents a service contract for checking if two users are connected.
    /// </summary>
    public interface IIsUsersConnectedService
    {
        /// <summary>
        /// Checks if two users are connected.
        /// </summary>
        /// <param name="userId">User which sended the request to connect. (Basically the first user)</param>
        /// <param name="connectedUserId">User which received request to connect. (Basically the second user)</param>
        /// <returns>IsUsersConnectedInfoToResponse object with info about is two users are connected</returns>
        Task<IsUsersConnectedInfoToResponse> IsUsersConnected(string userId, string connectedUserId);
    }
}
