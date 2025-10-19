using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Languages;
using FindACoach.Core.ServiceContracts.MyProfile.Languages;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FindACoach.Core.Services.MyProfile.Languages
{
    public class LanguagesEditorService : ILanguagesEditorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILanguagesRepository _languagesRepository;

        public LanguagesEditorService(IHttpContextAccessor httpContextAccessor, ILanguagesRepository languagesRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _languagesRepository = languagesRepository;
        }

        public async Task EditLanguage(EditLanguageDTO dto)
        {
            var principal = _httpContextAccessor.HttpContext?.User;
            if (principal == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            string? activeUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (activeUserId == null)
            {
                throw new UnauthorizedAccessException("Cannot resolve user id from claims");
            }

            await _languagesRepository.EditLanguage(dto, activeUserId);
        }
    }
}
