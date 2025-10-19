using FindACoach.Core.DTO.MyProfile.Languages;

namespace FindACoach.Core.ServiceContracts.MyProfile.Languages
{
    /// <summary>
    /// Represents the service for adding language.
    /// </summary>
    public interface ILanguagesAdderService
    {
        /// <summary>
        /// Adds language of the user.
        /// </summary>
        /// <param name="userId">User Id which language will be added.</param>
        /// <param name="dto">Data Transfer Object with data to add language.</param>
        /// <returns></returns>
        Task AddLanguage(string userId, AddLanguageDTO dto);
    }
}
