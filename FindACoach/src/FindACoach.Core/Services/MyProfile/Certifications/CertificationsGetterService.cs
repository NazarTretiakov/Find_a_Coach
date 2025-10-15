using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Certifications;
using FindACoach.Core.ServiceContracts.MyProfile.Certifications;

namespace FindACoach.Core.Services.MyProfile.Certifications
{
    public class CertificationsGetterService : ICertificationsGetterService
    {
        private readonly ICertificationsRepository _certificationsRepository;

        public CertificationsGetterService(ICertificationsRepository certificationsRepository)
        {
            _certificationsRepository = certificationsRepository;
        }
        public async Task<List<CertificationToResponse>> GetAllCertifications(string userId)
        {
            return await _certificationsRepository.GetAllCertifications(userId);
        }

        public async Task<List<CertificationToResponse>> GetLastTwoCertifications(string userId)
        {
            return await _certificationsRepository.GetLastTwoCertifications(userId);
        }

        public async Task<CertificationToResponse> GetCertification(string certificationId)
        {
            return await _certificationsRepository.GetCertification(certificationId);
        }
    }
}
