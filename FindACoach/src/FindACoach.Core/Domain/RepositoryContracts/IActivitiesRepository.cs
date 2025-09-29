using FindACoach.Core.DTO.MyProfile.Activities;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Activity entity.
    /// </summary>
    public interface IActivitiesRepository
    {
        /// <summary>
        /// Adds event to system.
        /// </summary>
        /// <param name="userId">User id which event will be added.</param>
        /// <param name="dto">Data Transfer Object with data of event to add.</param>
        /// <returns></returns>
        Task AddEvent(string userId, EventDTO dto);

        /// <summary>
        /// Adds survey to system.
        /// </summary>
        /// <param name="userId">User id which survey will be added.</param>
        /// <param name="dto">Data Transfer Object with data of survey to add.</param>
        /// <returns></returns>
        Task AddSurvey(string userId, SurveyDTO dto);

        /// <summary>
        /// Adds QA to system.
        /// </summary>
        /// <param name="userId">User id which QA will be added.</param>
        /// <param name="dto">Data Transfer Object with data of QA to add.</param>
        /// <returns></returns>
        Task AddQA(string userId, QADTO dto);

        /// <summary>
        /// Adds post to system.
        /// </summary>
        /// <param name="userId">User id which post will be added.</param>
        /// <param name="dto">Data Transfer Object with data of post to add.</param>
        /// <returns></returns>
        Task AddPost(string userId, PostDTO dto);

        /// <summary>
        /// Retrieves two last activities of user.
        /// </summary>
        /// <param name="userId">User id which activities will be retrieved.</param>
        /// <returns>ActivityCardToResponse object with data of activity.</returns>
        Task<List<ActivityCardToResponse>> GetLastTwoActivities(string userId);
    }
}
