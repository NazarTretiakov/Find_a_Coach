using FindACoach.Core.DTO;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing User entity.
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// Changes state (visibility and title) of "Complete profile" window
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="isVisible">Is the "Complete profile" window visible.</param>
        /// <param name="title">New title of "Complete profile" window.</param>
        /// <returns></returns>
        Task ChangeCompleteProfileWindowState(string userId, bool isVisible, string title);

        /// <summary>
        /// Gets state (visibility and title) of "Complete profile" window
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>"Complete profile" window DTO object.</returns>
        Task<CompleteProfileWindowStateDTO> GetCompleteProfileWindowState(string userId);
    }
}
