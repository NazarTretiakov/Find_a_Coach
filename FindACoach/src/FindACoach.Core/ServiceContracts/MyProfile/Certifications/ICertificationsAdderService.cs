using FindACoach.Core.DTO.MyProfile.Certifications;

namespace FindACoach.Core.ServiceContracts.MyProfile.Certifications
{
    /// <summary>
    /// Represents the service for adding certification.
    /// </summary>
    public interface ICertificationsAdderService
    {
        /// <summary>
        /// Adds certification of the user.
        /// </summary>
        /// <param name="userId">User Id which certification will be added.</param>
        /// <param name="dto">Data Transfer Object with data to add certification.</param>
        /// <returns></returns>
        Task AddCertification(string userId, AddCertificationDTO dto);
    }
}
