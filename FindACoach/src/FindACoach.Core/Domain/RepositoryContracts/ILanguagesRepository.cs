using FindACoach.Core.DTO.MyProfile.Languages;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Language entity.
    /// </summary>
    public interface ILanguagesRepository
    {
        /// <summary>
        /// Adds language of the user.
        /// </summary>
        /// <param name="userId">User Id which language will be added.</param>
        /// <param name="dto">Data Transfer Object with data to add language.</param>
        /// <returns></returns>
        Task AddLanguage(string userId, AddLanguageDTO dto);

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

        /// <summary>
        /// Edits existing language.
        /// </summary>
        /// <param name="dto">Data Transfer Object with data to edit language.</param>
        /// <param name="editorId">Id of user which edits the language (id of active user).</param>
        /// <returns></returns>
        Task EditLanguage(EditLanguageDTO dto, string editorId);


        /// <summary>
        /// Deletes language from the system.
        /// </summary>
        /// <param name="languageId">Language id which will be deleted.</param>
        /// <param name="activeUserId">Id of user which wants to delete language (id of active user).</param>
        /// <returns></returns>
        Task DeleteLanguage(string languageId, string activeUserId);
    }
}
