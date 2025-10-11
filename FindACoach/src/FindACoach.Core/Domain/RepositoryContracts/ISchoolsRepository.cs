using FindACoach.Core.DTO.MyProfile.Education;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing School entity.
    /// </summary>
    public interface ISchoolsRepository
    {
        /// <summary>
        /// Adds school of the user.
        /// </summary>
        /// <param name="userId">User Id which school will be added.</param>
        /// <param name="dto">Data Transfer Object with data to add school.</param>
        /// <returns></returns>
        Task AddSchool(string userId, AddSchoolDTO dto);

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

        /// <summary>
        /// Edits existing school.
        /// </summary>
        /// <param name="dto">Data Transfer Object with data to edit school.</param>
        /// <param name="editorId">Id of user which edits the school (id of active user).</param>
        /// <returns></returns>
        Task EditSchool(EditSchoolDTO dto, string editorId);


        /// <summary>
        /// Deletes school from the system.
        /// </summary>
        /// <param name="schoolId">School id which will be deleted.</param>
        /// <param name="activeUserId">Id of user which wants to delete school (id of active user).</param>
        /// <returns></returns>
        Task DeleteSchool(string schoolId, string activeUserId);
    }
}
