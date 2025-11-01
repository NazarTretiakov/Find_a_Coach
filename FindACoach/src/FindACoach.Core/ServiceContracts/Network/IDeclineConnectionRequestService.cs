using FindACoach.Core.DTO.Network;

namespace FindACoach.Core.ServiceContracts.Network
{
    /// <summary>
    /// Represents a service for declining connection requests between users.
    /// </summary>
    public interface IDeclineConnectionRequestService
    {
        /// <summary>
        /// Declines a connection request based on the provided ConnectionRequestIdDTO.
        /// </summary>
        /// <returns></returns>
        Task DeclineConnectionRequest(ConnectionRequestIdDTO dto);
    }
}
