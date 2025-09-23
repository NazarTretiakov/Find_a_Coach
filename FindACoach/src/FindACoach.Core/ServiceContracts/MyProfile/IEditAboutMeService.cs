using FindACoach.Core.DTO.MyProfile;

namespace FindACoach.Core.ServiceContracts.MyProfile
{
    /// <summary>
    /// Represents the service for editing "About me" information of user.
    /// </summary>
    public interface IEditAboutMeService
    {
        /// <summary>
        /// Edits "About me" information of user.
        /// </summary>
        /// <param name="userId">Id of user which information will be changed.</param>
        /// <param name="dto">DTO object with data to change information.</param>
        /// <returns></returns>
        Task EditAboutMe(string userId, AboutMeDTO dto);
    }
}
