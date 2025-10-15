using FindACoach.Core.DTO.MyProfile.Certifications;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Certification entity.
    /// </summary>
    public interface ICertificationsRepository
    {
        /// <summary>
        /// Adds certification of the user.
        /// </summary>
        /// <param name="userId">User Id which certification will be added.</param>
        /// <param name="dto">Data Transfer Object with data to add certification.</param>
        /// <returns></returns>
        Task AddCertification(string userId, AddCertificationDTO dto);

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

        /// <summary>
        /// Edits existing certification.
        /// </summary>
        /// <param name="dto">Data Transfer Object with data to edit certification.</param>
        /// <param name="editorId">Id of user which edits the certification (id of active user).</param>
        /// <returns></returns>
        Task EditCertification(EditCertificationDTO dto, string editorId);


        /// <summary>
        /// Deletes certification from the system.
        /// </summary>
        /// <param name="certificationId">Certification id which will be deleted.</param>
        /// <param name="activeUserId">Id of user which wants to delete certification (id of active user).</param>
        /// <returns></returns>
        Task DeleteCertification(string certificationId, string activeUserId);
    }
}
