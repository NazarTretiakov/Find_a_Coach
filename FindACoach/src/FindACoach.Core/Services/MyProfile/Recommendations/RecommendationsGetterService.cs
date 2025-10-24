using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Recommendations;
using FindACoach.Core.ServiceContracts.MyProfile.Recommendations;

namespace FindACoach.Core.Services.MyProfile.Recommendations
{
    public class RecommendationsGetterService : IRecommendationsGetterService
    {
        private readonly IRecommendationsRepository _recommendationsRepository;

        public RecommendationsGetterService(IRecommendationsRepository recommendationsRepository)
        {
            _recommendationsRepository = recommendationsRepository;
        }

        public async Task<List<RecommendationToResponse>> GetAllRecommendations(string userId)
        {
            var recommendations = await _recommendationsRepository.GetAllRecommendations(userId);

            return recommendations;
        }

        public async Task<List<RecommendationToResponse>> GetLastTwoRecommendations(string userId)
        {
            var recommendations = await _recommendationsRepository.GetLastTwoRecommendations(userId);

            return recommendations;
        }
    }
}
