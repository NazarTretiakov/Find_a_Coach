using FindACoach.Core.DTO.MyProfile.Settings;

namespace FindACoach.Core.ServiceContracts.MyProfile.Settings
{
    /// <summary>
    /// Represents the service for getting information of security settings.
    /// </summary>
    public interface ISecuritySettingsGetterService
    {
        /// <summary>
        /// Represents the service for retrieving information about condition of enabling of login notifications of current working user.
        /// </summary>
        /// <returns></returns>
        Task<IsLoginNotificationEnabledDTO> GetIsLoginNotificationEnabled();
    }
}
