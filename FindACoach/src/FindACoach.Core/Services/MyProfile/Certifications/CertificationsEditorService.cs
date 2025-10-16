using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Certifications;
using FindACoach.Core.ServiceContracts.MyProfile.Certifications;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FindACoach.Core.Services.MyProfile.Experience
{
    public class CertificationsEditorService : ICertificationsEditorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICertificationsRepository _certificationsRepository;

        public CertificationsEditorService(IHttpContextAccessor httpContextAccessor, ICertificationsRepository certificationsRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _certificationsRepository = certificationsRepository;
        }

        public async Task EditCertification(EditCertificationDTO dto)
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

            await _certificationsRepository.EditCertification(dto, activeUserId);
        }
    }
}
