using FindACoach.Core.DTO.MyProfile.Settings;

namespace FindACoach.Core.ServiceContracts.MyProfile.Settings
{
    /// <summary>
    /// Represents the service for editing security settings.
    /// </summary>
    public interface ISecuritySettingsEditorService
    {
        /// <summary>
        /// Edits condition of enabling login notification of current working user.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IsLoginNotificationEnabledDTO> EditIsLoginNotificationEnabled(IsLoginNotificationEnabledDTO dto);

        /// <summary>
        /// Edits security settings (IsLoginNotificationEnabled property and changes password) of current working user.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IsLoginNotificationEnabledDTO> EditSecuritySettings(EditSecuritySettingsDTO dto);
    }
}
