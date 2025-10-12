using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Education;
using FindACoach.Core.ServiceContracts.MyProfile.Education;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FindACoach.Core.Services.MyProfile.Experience
{
    public class SchoolsEditorService : ISchoolsEditorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISchoolsRepository _schoolsRepository;

        public SchoolsEditorService(IHttpContextAccessor httpContextAccessor, ISchoolsRepository schoolsRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _schoolsRepository = schoolsRepository;
        }

        public async Task EditSchool(EditSchoolDTO dto)
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

            await _schoolsRepository.EditSchool(dto, activeUserId);
        }
    }
}
