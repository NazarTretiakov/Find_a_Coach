using FindACoach.Core.DTO.MyProfile.Certifications;

namespace FindACoach.Core.ServiceContracts.MyProfile.Certifications
{
    /// <summary>
    /// Represents the service for editing certifications.
    /// </summary>
    public interface ICertificationsEditorService
    {
        /// <summary>
        /// Edits existing certification of user.
        /// </summary>
        /// <param name="dto">Data Transfer Object with data of certification to edit.</param>
        /// <returns></returns>
        Task EditCertification(EditCertificationDTO dto);
    }
}
