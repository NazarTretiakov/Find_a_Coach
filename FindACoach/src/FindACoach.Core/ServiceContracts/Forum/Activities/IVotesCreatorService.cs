using FindACoach.Core.Domain.Entities.Activity;

namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents the service for creating votes in surveys.
    /// </summary>
    public interface IVotesCreatorService
    {
        /// <summary>
        /// Creates a vote for a survey option by the current working user.
        /// </summary>
        /// <param name="optionId">Option id which user will vote for.</param>
        /// <returns></returns>
        Task<List<Vote>> VoteInSurvey(string optionId);
    }
}
