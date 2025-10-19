namespace FindACoach.Core.ServiceContracts.MyProfile.Languages
{
    /// <summary>
    /// Represents the service for deleting languages.
    /// </summary>
    public interface ILanguagesRemoverService
    {
        /// <summary>
        /// Deletes language from the system.
        /// </summary>
        /// <param name="languageId">Language id which will be deleted.</param>
        /// <returns></returns>
        Task DeleteLanguage(string languageId);
    }
}
