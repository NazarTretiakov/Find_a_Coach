using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Languages;
using FindACoach.Core.ServiceContracts.MyProfile.Languages;

namespace FindACoach.Core.Services.MyProfile.Languages
{
    public class LanguagesGetterService : ILanguagesGetterService
    {
        private readonly ILanguagesRepository _languagesRepository;

        public LanguagesGetterService(ILanguagesRepository languagesRepository)
        {
            _languagesRepository = languagesRepository;
        }
        public async Task<List<LanguageToResponse>> GetAllLanguages(string userId)
        {
            var languages = await _languagesRepository.GetAllLanguages(userId);

            return languages;
        }

        public async Task<List<LanguageToResponse>> GetTwoLanguages(string userId)
        {
            var languages = await _languagesRepository.GetTwoLanguages(userId);

            return languages;
        }

        public async Task<LanguageToResponse> GetLanguage(string languageId)
        {
            var language = await _languagesRepository.GetLanguage(languageId);

            return language;
        }
    }
}
