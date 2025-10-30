using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.ServiceContracts.Forum.Activities;

namespace FindACoach.Core.Services.Forum.Activities
{
    public class VotesGetterService : IVotesGetterService
    {
        private readonly IVotesRepository _votesRepository;

        public VotesGetterService(IVotesRepository votesRepository)
        {
            _votesRepository = votesRepository;
        }

        public async Task<List<Vote>> GetSurveyVotes(string surveyId)
        {
            return await _votesRepository.GetSurveyVotes(surveyId);
        }
    }
}
