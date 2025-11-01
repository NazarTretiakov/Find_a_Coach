using FindACoach.Core.DTO.Network;

namespace FindACoach.Core.ServiceContracts.Network
{
    /// <summary>
    /// Represents a service for sending connection requests between users.
    /// </summary>
    public interface IConnectionRequestSenderService
    {
        /// <summary>
        /// Sends a connection request based on the provided DTO.
        /// </summary>
        /// <returns></returns>
        Task SendConnectionRequest(ConnectionRequestDTO dto);
    }
}
