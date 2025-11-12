using FindACoach.Core.DTO.Network;

namespace FindACoach.Core.ServiceContracts.Network
{
    /// <summary>
    /// Represents the service for retrieving users from the system.
    /// </summary>
    public interface IUsersGetterService
    {
        /// <summary>
        /// Retrieves filtered users.
        /// </summary>
        /// <param name="searchString">Search string to filter the users..</param>
        /// <returns></returns>
        Task<List<ConnectionToResponse>> GetFilteredUsers(string searchString, int page, int pageSize);

        /// <summary>
        /// Retrieves recommended users for user.
        /// </summary>
        /// <param name="userId">User id which recommended users will be retrieved.</param>
        /// <returns></returns>
        Task<List<ConnectionToResponse>> GetRecommendedUsers(string userId, int page, int pageSize);
    }
}
