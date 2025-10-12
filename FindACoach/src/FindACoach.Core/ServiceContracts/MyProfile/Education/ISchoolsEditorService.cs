using FindACoach.Core.DTO.MyProfile.Education;

namespace FindACoach.Core.ServiceContracts.MyProfile.Education
{
    /// <summary>
    /// Represents the service for editing schools.
    /// </summary>
    public interface ISchoolsEditorService
    {
        /// <summary>
        /// Edits existing school of user.
        /// </summary>
        /// <param name="dto">Data Transfer Object with data of school to edit.</param>
        /// <returns></returns>
        Task EditSchool(EditSchoolDTO dto);
    }
}
