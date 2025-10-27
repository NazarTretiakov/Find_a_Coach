using FindACoach.Core.DTO.Forum;

namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents the service for creating answer on QA.
    /// </summary>
    public interface IQAAnswersCreatorService
    {
        /// <summary>
        /// Adds a answer on QA by current working user to the system.
        /// </summary>
        /// <param name="dto">The data transfer object that contains information about the answer on QA that will be created.</param>
        /// <returns>QAAnswerToResponse object which contains data of created answer on QA.</returns>
        Task LeaveQAAnswer(LeaveQAAnswerDTO dto);
    }
}
