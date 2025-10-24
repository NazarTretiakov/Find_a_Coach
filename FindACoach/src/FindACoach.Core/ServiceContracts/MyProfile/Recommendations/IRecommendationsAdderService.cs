using FindACoach.Core.DTO.MyProfile.Recommendations;

namespace FindACoach.Core.ServiceContracts.MyProfile.Recommendations
{
    /// <summary>
    /// Represents the service for adding recommendation.
    /// </summary>
    public interface IRecommendationsAdderService
    {
        /// <summary>
        /// Adds recommendation of the user.
        /// </summary>=
        /// <param name="dto">Data Transfer Object with data to add recommendation.</param>
        /// <returns></returns>
        Task AddRecommendation(AddRecommendationDTO dto);
    }
}
