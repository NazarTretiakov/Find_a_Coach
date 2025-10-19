using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Languages;
using FindACoach.Core.ServiceContracts.MyProfile.Languages;

namespace FindACoach.Core.Services.MyProfile.Languages
{
    public class LanguagesAdderService : ILanguagesAdderService
    {
        private readonly ILanguagesRepository _languagesRepository;

        public LanguagesAdderService(ILanguagesRepository languagesRepository)
        {
            _languagesRepository = languagesRepository;
        }

        public async Task AddLanguage(string userId, AddLanguageDTO dto)
        {
            await _languagesRepository.AddLanguage(userId, dto);
        }
    }
}
