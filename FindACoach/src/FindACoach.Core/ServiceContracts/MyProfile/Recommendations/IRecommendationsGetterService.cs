using FindACoach.Core.DTO.MyProfile.Recommendations;

namespace FindACoach.Core.ServiceContracts.MyProfile.Recommendations
{
    /// <summary>
    /// Represents the service for getting recommendations.
    /// </summary>
    public interface IRecommendationsGetterService
    {
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
    }
}
