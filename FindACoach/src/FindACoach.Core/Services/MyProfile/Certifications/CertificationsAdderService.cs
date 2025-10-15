using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Certifications;
using FindACoach.Core.ServiceContracts.MyProfile.Certifications;

namespace FindACoach.Core.Services.MyProfile.Certifications
{
    public class CertificationsAdderService : ICertificationsAdderService
    {
        private readonly ICertificationsRepository _certificationsRepository;

        public CertificationsAdderService(ICertificationsRepository certificationsRepository)
        {
            _certificationsRepository = certificationsRepository;
        }

        public async Task AddCertification(string userId, AddCertificationDTO dto)
        {
            await _certificationsRepository.AddCertification(userId, dto);
        }
    }
}
