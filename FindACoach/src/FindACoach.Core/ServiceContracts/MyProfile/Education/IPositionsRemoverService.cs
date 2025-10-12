namespace FindACoach.Core.ServiceContracts.MyProfile.Education
{
    /// <summary>
    /// Represents the service for deleting schools.
    /// </summary>
    public interface ISchoolsRemoverService
    {
        /// <summary>
        /// Deletes school from the system.
        /// </summary>
        /// <param name="schoolId">School id which will be deleted.</param>
        /// <returns></returns>
        Task DeleteSchool(string schoolId);
    }
}
