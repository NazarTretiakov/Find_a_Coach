using FindACoach.Core.DTO.MyProfile.Settings;

namespace FindACoach.Core.ServiceContracts.MyProfile.Settings
{
    /// <summary>
    /// Represents the service for editing contact infomation visibility of current working user.
    /// </summary>
    public interface IContactInformationVisibilityEditorService
    {
        /// <summary>
        /// Edits contact information visibility of current working user.
        /// </summary>
        /// <param name="contactInformationVisibilityType">Contact information visibility type which will be settled.</param>
        /// <returns></returns>
        Task<ContactInformationVisibilityToResponse> Edit(string contactInformationVisibilityType);
    }
}
