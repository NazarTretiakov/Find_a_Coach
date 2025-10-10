using FindACoach.Core.DTO.MyProfile.Experience;

namespace FindACoach.Core.ServiceContracts.MyProfile.Experience
{
    /// <summary>
    /// Represents the service for getting positions.
    /// </summary>
    public interface IPositionsGetterService
    {
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
    }
}
