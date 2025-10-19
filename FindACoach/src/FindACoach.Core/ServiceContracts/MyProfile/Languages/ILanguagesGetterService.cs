using FindACoach.Core.DTO.MyProfile.Languages;

namespace FindACoach.Core.ServiceContracts.MyProfile.Languages
{
    /// <summary>
    /// Represents the service for getting languages.
    /// </summary>
    public interface ILanguagesGetterService
    {
        /// <summary>
        /// Retrieves languages with best proficiency level of user.
        /// </summary>
        /// <param name="userId">User id which languages will be retrieved.</param>
        /// <returns></returns>
        Task<List<LanguageToResponse>> GetTwoLanguages(string userId);

        /// <summary>
        /// Retrieves all languages of user.
        /// </summary>
        /// <param name="userId">User id which languages will be retrieved.</param>
        /// <returns></returns>
        Task<List<LanguageToResponse>> GetAllLanguages(string userId);

        /// <summary>
        /// Retrieves language by id.
        /// </summary>
        /// <param name="languageId">Language id which will be retrieved.</param>
        /// <returns></returns>
        Task<LanguageToResponse> GetLanguage(string languageId);
    }
}
