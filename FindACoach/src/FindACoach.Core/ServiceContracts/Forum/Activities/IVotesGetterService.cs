using FindACoach.Core.Domain.Entities.Activity;

namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents the service for retrieving votes of survey.
    /// </summary>
    public interface IVotesGetterService
    {
        /// <summary>
        /// Retrieves all votes of survey.
        /// </summary>
        /// <param name="surveyId">Survey Id which votes will be retrieved</param>
        /// <returns></returns>
        Task<List<Vote>> GetSurveyVotes(string surveyId);
    }
}
