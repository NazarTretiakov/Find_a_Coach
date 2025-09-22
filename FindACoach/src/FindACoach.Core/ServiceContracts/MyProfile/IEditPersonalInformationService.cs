using FindACoach.Core.DTO.MyProfile;

namespace FindACoach.Core.ServiceContracts.MyProfile
{
    /// <summary>
    /// Represents the service for editing personal information of user.
    /// </summary>
    public interface IEditPersonalInformationService
    {
        /// <summary>
        /// Edits personal information of user.
        /// </summary>
        /// <param name="userId">Id of user which personal information will be changed.</param>
        /// <param name="dto">DTO object with data to change personal information.</param>
        /// <returns></returns>
        Task EditPersonalInformation(string userId, EditPersonalInformationDTO dto);
    }
}
