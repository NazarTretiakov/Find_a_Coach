using FindACoach.Core.DTO.MyProfile.Settings;

namespace FindACoach.Core.ServiceContracts.MyProfile.Settings
{
    /// <summary>
    /// Represents the service for retrieving profile image path of user.
    /// </summary>
    public interface IProfileImageGetterService
    {
        /// <summary>
        /// Retrieves profile image path of currect working user.
        /// </summary>
        /// <returns>ProfileImagePathToResponse object with info about profile image path of currect working user.</returns>
        Task<ProfileImagePathToResponse> Get();
    }
}

