using FindACoach.Core.DTO.MyProfile.Experience;

namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents the service for adding position.
    /// </summary>
    public interface IPositionsAdderService
    {
        /// <summary>
        /// Adds position of the user.
        /// </summary>
        /// <param name="userId">User Id which position will be added.</param>
        /// <param name="dto">Data Transfer Object with data to add position.</param>
        /// <returns></returns>
        Task AddPosition(string userId, AddPositionDTO dto);
    }
}
