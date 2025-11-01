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
    }
}
