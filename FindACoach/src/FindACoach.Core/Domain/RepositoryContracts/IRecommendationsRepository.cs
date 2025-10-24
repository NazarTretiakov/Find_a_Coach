using FindACoach.Core.DTO.MyProfile.Recommendations;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Recommendation entity.
    /// </summary>
    public interface IRecommendationsRepository
    {
        /// <summary>
        /// Adds recommendation of the user.
        /// </summary>
        /// <param name="userId">User id which created recommendation (active user id).</param>
        /// <param name="dto">Data Transfer Object with data to add recommendation.</param>
        /// <returns></returns>
        Task AddRecommendation(string userId, AddRecommendationDTO dto);

        /// <summary>
        /// Retrieves last two recommendations received and given of user (total amount of recommendations retrieved is 4).
        /// </summary>
        /// <param name="userId">User id which recommendations will be retrieved.</param>
        /// <returns></returns>
        Task<List<RecommendationToResponse>> GetLastTwoRecommendations(string userId);

        /// <summary>
        /// Retrieves all recommendations of user.
        /// </summary>
        /// <param name="userId">User id which recommendations will be retrieved.</param>
        /// <returns></returns>
        Task<List<RecommendationToResponse>> GetAllRecommendations(string userId);

        /// <summary>
        /// Deletes recommendation from the system.
        /// </summary>
        /// <param name="userId">User id which created recommendation (active user id).</param>
        /// <param name="recommendationId">Recommendation id which will be deleted.</param>
        /// <returns></returns>
        Task DeleteRecommendation(string userId, string recommendationId);
    }
}
