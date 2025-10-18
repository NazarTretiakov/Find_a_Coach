using FindACoach.Core.DTO.MyProfile.Skills;

namespace FindACoach.Core.ServiceContracts.MyProfile.Skills
{
    /// <summary>
    /// Represents the service for getting skills.
    /// </summary>
    public interface ISkillsGetterService
    {
        /// <summary>
        /// Retrieves two skills of user with the most usages.
        /// </summary>
        /// <param name="userId">User id which skills will be retrieved.</param>
        /// <returns></returns>
        Task<List<SkillToResponse>> GetTwoSkillsWithMostUsages(string userId);

        /// <summary>
        /// Retrieves all skills of user.
        /// </summary>
        /// <param name="userId">User id which skills will be retrieved.</param>
        /// <returns></returns>
        Task<List<SkillToResponse>> GetAllSkills(string userId);
    }
}