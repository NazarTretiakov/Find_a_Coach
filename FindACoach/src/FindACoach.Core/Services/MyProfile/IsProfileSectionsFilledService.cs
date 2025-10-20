using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile;
using FindACoach.Core.ServiceContracts.MyProfile;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FindACoach.Core.Services.MyProfile
{
    public class IsProfileSectionsFilledService : IIsProfileSectionsCompletedService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IsProfileSectionsFilledService(IUsersRepository usersRepository, IHttpContextAccessor httpContextAccessor)
        {
            _usersRepository = usersRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IsProfileSectionsCompletedToResponse> IsProfileSectionsCompleted()
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

            return await _usersRepository.IsProfileSectionsCompleted(activeUserId);
        }
    }
}
