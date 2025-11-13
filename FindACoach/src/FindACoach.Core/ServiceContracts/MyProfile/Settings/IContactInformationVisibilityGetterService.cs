using FindACoach.Core.DTO.MyProfile.Settings;

namespace FindACoach.Core.ServiceContracts.MyProfile.Settings
{
    /// <summary>
    /// Edits contact information visibility of current working user.
    /// </summary>
    public interface IContactInformationVisibilityGetterService
    {
        /// <summary>
        /// Retrieves contact information visibility of current working user.
        /// </summary>
        /// <returns></returns>
        Task<ContactInformationVisibilityToResponse> Get();


        /// <summary>
        /// Retrieves contact information visibility of user with specified userId.
        /// </summary>
        /// <returns></returns>
        Task<ContactInformationVisibilityToResponse> Get(string userId);
    }
}
