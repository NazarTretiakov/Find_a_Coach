using FindACoach.Core.DTO.MyProfile.Education;

namespace FindACoach.Core.ServiceContracts.MyProfile.Education
{
    /// <summary>
    /// Represents the service for getting schools.
    /// </summary>
    public interface ISchoolsGetterService
    {
        /// <summary>
        /// Retrieves two last schools of user.
        /// </summary>
        /// <param name="userId">User id which schools will be retrieved.</param>
        /// <returns></returns>
        Task<List<SchoolToResponse>> GetLastTwoSchools(string userId);

        /// <summary>
        /// Retrieves all schools of user.
        /// </summary>
        /// <param name="userId">User id which schools will be retrieved.</param>
        /// <returns></returns>
        Task<List<SchoolToResponse>> GetAllSchools(string userId);

        /// <summary>
        /// Retrieves school by id.
        /// </summary>
        /// <param name="schoolId">School id which will be retrieved.</param>
        /// <returns></returns>
        Task<SchoolToResponse> GetSchool(string schoolId);
    }
}
