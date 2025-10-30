using FindACoach.Core.Domain.Entities.Activity;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Vote entity.
    /// </summary>
    public interface IVotesRepository
    {

        /// <summary>
        /// Retrieves all votes of survey.
        /// </summary>
        /// <param name="surveyId">Survey Id which votes will be retrieved</param>
        /// <returns></returns>
        Task<List<Vote>> GetSurveyVotes(string surveyId);


        /// <summary>
        /// Creates a vote for a survey option by the current working user.
        /// </summary>
        /// <param name="vote">Vote object that will be added.</param>
        /// <returns></returns>
        Task<List<Vote>> Add(Vote vote);
    }
}
