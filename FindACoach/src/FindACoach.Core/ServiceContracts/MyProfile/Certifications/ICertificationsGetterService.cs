using FindACoach.Core.DTO.MyProfile.Certifications;

namespace FindACoach.Core.ServiceContracts.MyProfile.Certifications
{
    /// <summary>
    /// Represents the service for getting certifications.
    /// </summary>
    public interface ICertificationsGetterService
    {
        /// <summary>
        /// Retrieves two last certifications of user.
        /// </summary>
        /// <param name="userId">User id which certifications will be retrieved.</param>
        /// <returns></returns>
        Task<List<CertificationToResponse>> GetLastTwoCertifications(string userId);

        /// <summary>
        /// Retrieves all certifications of user.
        /// </summary>
        /// <param name="userId">User id which certifications will be retrieved.</param>
        /// <returns></returns>
        Task<List<CertificationToResponse>> GetAllCertifications(string userId);

        /// <summary>
        /// Retrieves certification by id.
        /// </summary>
        /// <param name="certificationId">Certification id which will be retrieved.</param>
        /// <returns></returns>
        Task<CertificationToResponse> GetCertification(string certificationId);
    }
}
