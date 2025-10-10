using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Experience;
using FindACoach.Core.ServiceContracts.MyProfile.Experience;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FindACoach.Core.Services.MyProfile.Experience
{
    public class PositionsEditorService : IPositionsEditorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPositionsRepository _positionsRepository;

        public PositionsEditorService(IHttpContextAccessor httpContextAccessor, IPositionsRepository positionsRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _positionsRepository = positionsRepository;
        }

        public async Task EditPosition(EditPositionDTO dto)
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

            await _positionsRepository.EditPosition(dto, activeUserId);
        }
    }
}
