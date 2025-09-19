using FindACoach.Core.DTO.MyProfile;

namespace FindACoach.Core.ServiceContracts.MyProfile
{
    /// <summary>
    /// Represents the service for getting personal information.
    /// </summary>
    public interface IGetPersonalInformationService
    {
        /// <summary>
        /// Retrieves personal information of user.
        /// </summary>
        /// <param name="userId">User id which personal information will be retrieved.</param>
        /// <returns>PersonalInformationToResponse object with personal information.</returns>
        Task<PersonalInformationToResponse> GetPersonalInformation(string userId);
    }
}
