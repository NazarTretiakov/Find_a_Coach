namespace FindACoach.Core.ServiceContracts.MyProfile.Recommendations
{
    /// <summary>
    /// Represents the service for deleting recommendations.
    /// </summary>
    public interface IRecommendationsRemoverService
    {
        /// <summary>
        /// Deletes recommendation from the system.
        /// </summary>
        /// <param name="recommendationId">Recommendation id which will be deleted.</param>
        /// <returns></returns>
        Task DeleteRecommendation(string recommendationId);
    }
}
