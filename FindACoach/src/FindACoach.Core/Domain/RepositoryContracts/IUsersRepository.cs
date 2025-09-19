using FindACoach.Core.DTO;
using FindACoach.Core.DTO.MyProfile;

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

        /// <summary>
        /// Edits personal information of user (firstname, lastname, primary occupation, headline, location, phone number, websites)
        /// </summary>
        /// <param name="userId">Id of user which personal information will be edited.</param>
        /// <param name="dto">DTO object with data to change personal information.</param>
        /// <returns></returns>
        Task EditPersonalInformation(string userId, EditPersonalInformationDTO dto);

        /// <summary>
        /// Retrieves personal information of user.
        /// </summary>
        /// <param name="userId">Id of user which information will be retrieved.</param>
        /// <returns>PersonalInformationToResponse object with personal information.</returns>
        Task<PersonalInformationToResponse> GetPersonalInformation(string userId);
    }
}
