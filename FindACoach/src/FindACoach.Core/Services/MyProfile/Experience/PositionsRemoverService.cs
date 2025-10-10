using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FindACoach.Core.Services.MyProfile.Experience
{
    public class PositionsRemoverService : IPositionsRemoverService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPositionsRepository _positionsRepository;

        public PositionsRemoverService(IHttpContextAccessor httpContextAccessor, IPositionsRepository positionsRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _positionsRepository = positionsRepository;
        }

        public async Task DeletePosition(string positionId)
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

            await _positionsRepository.DeletePosition(positionId, activeUserId);
        }
    }
}
