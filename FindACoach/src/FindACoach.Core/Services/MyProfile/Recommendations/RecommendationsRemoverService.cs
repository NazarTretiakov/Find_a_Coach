using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.ServiceContracts.MyProfile.Recommendations;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FindACoach.Core.Services.MyProfile.Recommendations
{
    public class RecommendationsRemoverService : IRecommendationsRemoverService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRecommendationsRepository _recommendationsRepository;

        public RecommendationsRemoverService(IHttpContextAccessor httpContextAccessor, IRecommendationsRepository recommendationsRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _recommendationsRepository = recommendationsRepository;
        }

        public async Task DeleteRecommendation(string recommendationId)
        {
            var principal = _httpContextAccessor.HttpContext?.User;
            if (principal == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            string? activeUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (activeUserId == null)
            {
                throw new UnauthorizedAccessException("Cannot resolve user id from claims");
            }

            await _recommendationsRepository.DeleteRecommendation(activeUserId, recommendationId);
        }
    }
}