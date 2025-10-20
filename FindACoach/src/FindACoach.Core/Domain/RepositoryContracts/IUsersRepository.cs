﻿using FindACoach.Core.DTO.MyProfile;

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

        /// <summary>
        /// Retrieves personal and contact information of user.
        /// </summary>
        /// <param name="userId">User id which personal information will be retrieved.</param>
        /// <returns>PersonalAndContactInformationToResponse object with personal and contact information of user.</returns>
        Task<PersonalAndContactInformationToResponse> GetPersonalAndContactInformation(string userId);

        /// <summary>
        /// Edits "About me" information of user.
        /// </summary>
        /// <param name="userId">Id of user which information will be changed.</param>
        /// <param name="dto">DTO object with data to change information.</param>
        /// <returns></returns>
        Task EditAboutMe(string userId, AboutMeDTO dto);

        /// <summary>
        /// Retrieves "About me" information of user.
        /// </summary>
        /// <param name="userId">User id which information will be retrieved.</param>
        /// <returns>AboutMeDTO object with information.</returns>
        Task<AboutMeDTO> GetAboutMe(string userId);

        /// <summary>
        /// Determines whether all required sections of the user's profile are filled.
        /// </summary>
        /// <returns>IsProfileSectionsFilledToResponse object with info about if the each section is filled or not.</returns>
        Task<IsProfileSectionsCompletedToResponse> IsProfileSectionsCompleted(string activeUserId);
    }
}
