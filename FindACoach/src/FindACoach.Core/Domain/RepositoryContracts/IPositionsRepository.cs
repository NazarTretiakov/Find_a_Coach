using FindACoach.Core.DTO.MyProfile.Experience;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Position entity.
    /// </summary>
    public interface IPositionsRepository
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
