using FindACoach.Core.DTO.Network;

namespace FindACoach.Core.ServiceContracts.Network
{
    /// <summary>
    /// Represents service contract for retrieing the information about network overview of user.
    /// </summary>
    public interface INetworkOverviewGetterService
    {
        /// <summary>
        /// Retrieves the information about network overview of user.
        /// </summary>
        /// <param name="userId">User id which information about network will be retrieved.</param>
        /// <returns></returns>
        Task<NetworkOverviewInfoToResponse> GetNetworkOverview(string userId);
    }
}
