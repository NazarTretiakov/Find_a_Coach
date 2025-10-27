using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.DTO.Forum;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing QAAnswer entity.
    /// </summary>
    public interface IQAAnswersRepository
    {
        /// <summary>
        /// Adds answer on QA to the system.
        /// </summary>
        /// <param name="QAAnswer">QAAnswer object that will be added.</param>
        /// <returns></returns>
        Task Add(QAAnswer QAAnswer);

        /// <summary>
        /// Retrieves all asnwers of QA.
        /// </summary>
        /// <param name="QAId">QA id which answers will be retrieved.</param>
        /// <returns></returns>
        Task<List<QAAnswerToResponse>> GetQAAnswers(string QAId);
    }
}
