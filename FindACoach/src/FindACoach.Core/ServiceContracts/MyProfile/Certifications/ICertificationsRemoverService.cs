namespace FindACoach.Core.ServiceContracts.MyProfile.Certifications
{
    /// <summary>
    /// Represents the service for deleting certifications.
    /// </summary>
    public interface ICertificationsRemoverService
    {
        /// <summary>
        /// Deletes certification from the system.
        /// </summary>
        /// <param name="certificationId">Certification id which will be deleted.</param>
        /// <returns></returns>
        Task DeleteCertification(string certificationId);
    }
}
