using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Settings;
using FindACoach.Core.ServiceContracts.MyProfile.Settings;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FindACoach.Core.Services.MyProfile.Settings
{
    public class SecuritySettingsEditorService : ISecuritySettingsEditorService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecuritySettingsEditorService(IUsersRepository usersRepository, IHttpContextAccessor httpContextAccessor)
        {
            _usersRepository = usersRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IsLoginNotificationEnabledDTO> EditIsLoginNotificationEnabled(IsLoginNotificationEnabledDTO dto)
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

            return await _usersRepository.EditIsLoginNotificationEnabled(activeUserId, dto);
        }

        public async Task<IsLoginNotificationEnabledDTO> EditSecuritySettings(EditSecuritySettingsDTO dto)
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

            return await _usersRepository.EditSecuritySettings(activeUserId, dto);
        }
    }
}
