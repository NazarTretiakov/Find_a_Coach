using FindACoach.Core.DTO.Forum;

namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents the service for retrieving answers of QA.
    /// </summary>
    public interface IQAAnswersGetterService
    {
        /// <summary>
        /// Retrieves all answers of QA.
        /// </summary>
        /// <param name="QAId">QA id which answers will be retrieved.</param>
        /// <returns></returns>
        Task<List<QAAnswerToResponse>> GetQAAnswers(string QAId);
    }
}
