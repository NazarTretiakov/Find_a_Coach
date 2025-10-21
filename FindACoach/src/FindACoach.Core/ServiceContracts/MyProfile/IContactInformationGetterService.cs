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
        /// <returns>ContactInformationToResponse object with contact information.</returns>
        Task<ContactInformationToResponse> GetContactInformation();
    }
}
