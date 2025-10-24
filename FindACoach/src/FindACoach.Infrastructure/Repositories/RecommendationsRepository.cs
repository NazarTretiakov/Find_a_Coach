using FindACoach.Core.Domain.Entities.User;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Languages;
using FindACoach.Core.DTO.MyProfile.Recommendations;
using FindACoach.Core.Enums;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FindACoach.Infrastructure.Repositories
{
    public class RecommendationsRepository: IRecommendationsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public RecommendationsRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task AddRecommendation(string userId, AddRecommendationDTO dto)
        {
            if (userId != dto.RecommenderUserId)
            {
                throw new UnauthorizedAccessException("Only authenticated user can leave recommendations.");
            }

            if (dto.RecommenderUserId == dto.RecommendedUserId)
            {
                throw new ArgumentException("You can't recommend yourself.");
            }

            Recommendation recommendation = new Recommendation()
            {
                Id = Guid.NewGuid(),
                RecommenderUserId = Guid.Parse(userId),
                RecommendedUserId = Guid.Parse(dto.RecommendedUserId),
                Content = dto.Content,
                DateOfCreation = DateTime.Now
            };

            await _db.AddAsync(recommendation);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch(Exception e)
            {

            }
        }

        public async Task DeleteRecommendation(string userId, string recommendationId)
        {
            Recommendation recommendation = await _db.Recommendations.FirstOrDefaultAsync(r => r.Id == Guid.Parse(recommendationId));

            if (recommendation == null)
            {
                throw new ArgumentException("Recommendation with specified id doesn't exist.");
            }

            if (recommendation.RecommenderUserId.ToString() != userId)
            {
                throw new UnauthorizedAccessException("Only creator of recommendation delete it.");
            }

             _db.Recommendations.Remove(recommendation);

            await _db.SaveChangesAsync();
        }

        public async Task<List<RecommendationToResponse>> GetAllRecommendations(string userId)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            var recommendations = await _db.Recommendations
                .Where(r => r.RecommenderUserId == Guid.Parse(userId) || r.RecommendedUserId == Guid.Parse(userId))
                .OrderByDescending(r => r.DateOfCreation)
                .Select(r => new RecommendationToResponse()
                {
                    RecommenderUserProfileImagePath = $"{serverUrl}/Images/UserProfiles/{r.RecommenderUser.ImagePath}",
                    RecommenderUserFirstName = r.RecommenderUser.FirstName,
                    RecommenderUserLastName = r.RecommenderUser.LastName,
                    RecommenderUserHeadline = r.RecommenderUser.Headline,
                    RecommendationId = r.Id,
                    RecommenderUserId = r.RecommenderUserId,
                    RecommendedUserId = r.RecommendedUserId,
                    Content = r.Content,
                    DateOfCreation = r.DateOfCreation
                })
                .ToListAsync();

            return recommendations;
        }

        public async Task<List<RecommendationToResponse>> GetLastTwoRecommendations(string userId)
        {
            var recommendations = await GetAllRecommendations(userId);

            var lastTwoGivenRecommendations = recommendations
                .Where(r => r.RecommenderUserId.ToString() == userId)
                .OrderByDescending(r => r.DateOfCreation)
                .Take(2)
                .ToList();

            var lastTwoReceivedRecommendations = recommendations
                .Where(r => r.RecommendedUserId.ToString() == userId)
                .OrderByDescending(r => r.DateOfCreation)
                .Take(2)
                .ToList();

            var recommendationsToReturn = lastTwoGivenRecommendations.Concat(lastTwoReceivedRecommendations).ToList();

            return recommendationsToReturn;
        }
    }
}
