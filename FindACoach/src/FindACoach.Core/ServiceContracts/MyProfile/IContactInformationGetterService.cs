using FindACoach.Core.DTO.MyProfile;

namespace FindACoach.Core.ServiceContracts.MyProfile
{
    /// <summary>
    /// Represents the service for getting contact information.
    /// </summary>
    public interface IContactInformationGetterService
    {
        /// <summary>
        /// Retrieves contact information of user.
        /// </summary>
        /// <param name="userId">User id which contact information will be retrieved</param>
        /// <returns>ContactInformationToResponse object with contact information.</returns>
        Task<ContactInformationToResponse> GetContactInformation(string userId);
    }
}
