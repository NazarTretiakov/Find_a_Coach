using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FindACoach.Core.Services.Forum.Activities
{
    public class VotesCreatorService : IVotesCreatorService
    {
        private readonly IVotesRepository _votesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VotesCreatorService(IVotesRepository votesRepository, IHttpContextAccessor httpContextAccessor)
        {
            _votesRepository = votesRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<List<Vote>> VoteInSurvey(string optionId)
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

            Vote vote = new Vote
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(activeUserId),
                SurveyOptionId = Guid.Parse(optionId)
            };

            return _votesRepository.Add(vote);
        }
    }
}
