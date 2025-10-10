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

        /// <summary>
        /// Retrieves two last positions of user.
        /// </summary>
        /// <param name="userId">User id which positions will be retrieved.</param>
        /// <returns></returns>
        Task<List<PositionToResponse>> GetLastTwoPositions(string userId);

        /// <summary>
        /// Retrieves all positions of user.
        /// </summary>
        /// <param name="userId">User id which positions will be retrieved.</param>
        /// <returns></returns>
        Task<List<PositionToResponse>> GetAllPositions(string userId);

        /// <summary>
        /// Retrieves position by id.
        /// </summary>
        /// <param name="positionId">Position id which will be retrieved.</param>
        /// <returns></returns>
        Task<PositionToResponse> GetPosition(string positionId);

        /// <summary>
        /// Edits existing position.
        /// </summary>
        /// <param name="dto">Data Transfer Object with data to edit position.</param>
        /// <param name="editorId">Id of user which edits the position (id of active user).</param>
        /// <returns></returns>
        Task EditPosition(EditPositionDTO dto, string editorId);


        /// <summary>
        /// Deletes position from the system.
        /// </summary>
        /// <param name="positionId">Position id which will be deleted.</param>
        /// <param name="activeUserId">Id of user which wants to delete position (id of active user).</param>
        /// <returns></returns>
        Task DeletePosition(string positionId, string activeUserId);
    }
}
