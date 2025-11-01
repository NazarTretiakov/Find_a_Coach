using FindACoach.Core.DTO.Network;

namespace FindACoach.Core.ServiceContracts.Network
{
    /// <summary>
    /// Represents a service for accepting connection requests between users.
    /// </summary>
    public interface IAcceptConnectionRequestService
    {
        /// <summary>
        /// Accepts a connection request based on the provided ConnectionRequestIdDTO.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task AcceptConnectionRequest(ConnectionRequestIdDTO dto);
    }
}
