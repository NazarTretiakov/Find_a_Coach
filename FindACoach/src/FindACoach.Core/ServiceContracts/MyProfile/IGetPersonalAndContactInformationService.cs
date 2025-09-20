using FindACoach.Core.DTO.MyProfile;

namespace FindACoach.Core.ServiceContracts.MyProfile
{
    /// <summary>
    /// Represents the service for getting personal and contact information in one object.
    /// </summary>
    public interface IGetPersonalAndContactInformationService
    {
        /// <summary>
        /// Retrieves personal and contact information of user.
        /// </summary>
        /// <param name="userId">User id which personal information will be retrieved.</param>
        /// <returns>PersonalAndContactInformationToResponse object with personal and contact information of user.</returns>
        Task<PersonalAndContactInformationToResponse> GetPersonalAndContactInformation(string userId);
    }
}
