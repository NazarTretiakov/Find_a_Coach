using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Settings;
using FindACoach.Core.ServiceContracts.MyProfile.Settings;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FindACoach.Core.Services.MyProfile.Settings
{
    public class ProfileImageGetterService : IProfileImageGetterService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileImageGetterService(IUsersRepository usersRepository, IHttpContextAccessor httpContextAccessor)
        {
            _usersRepository = usersRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProfileImagePathToResponse> Get()
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

            return await _usersRepository.GetProfileImagePath(activeUserId);
        }
    }
}
