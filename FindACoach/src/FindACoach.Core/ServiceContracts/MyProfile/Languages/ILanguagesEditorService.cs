using FindACoach.Core.DTO.MyProfile.Languages;

namespace FindACoach.Core.ServiceContracts.MyProfile.Languages
{
    /// <summary>
    /// Represents the service for editing languages.
    /// </summary>
    public interface ILanguagesEditorService
    {
        /// <summary>
        /// Edits existing language.
        /// </summary>
        /// <param name="dto">Data Transfer Object with data of language to edit.</param>
        /// <returns></returns>
        Task EditLanguage(EditLanguageDTO dto);
    }
}
