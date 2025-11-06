using FindACoach.Core.DTO.Network;

namespace FindACoach.Core.ServiceContracts.Network
{
    /// <summary>
    /// Represents the service for removing connections.
    /// </summary>
    public interface IConnectionsRemoverService
    {
        /// <summary>
        /// Removes connection from the system.
        /// </summary>
        /// <param name="dto">Data Transfer Object with connection information to remove.</param>
        /// <returns></returns>
        Task RemoveConnection(RemoveConnectionDTO dto);
    }
}
