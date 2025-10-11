using FindACoach.Core.DTO.MyProfile.Education;

namespace FindACoach.Core.ServiceContracts.MyProfile.Education
{
    /// <summary>
    /// Represents the service for adding school.
    /// </summary>
    public interface ISchoolsAdderService
    {
        /// <summary>
        /// Adds school of the user.
        /// </summary>
        /// <param name="userId">User Id which school will be added.</param>
        /// <param name="dto">Data Transfer Object with data to add school.</param>
        /// <returns></returns>
        Task AddSchool(string userId, AddSchoolDTO dto);
    }
}
