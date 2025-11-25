using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.DTO.MyProfile.Activities;
using System.Linq.Expressions;

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

        /// <summary>
        /// Retrieves all activities of user.
        /// </summary>
        /// <param name="userId">User id which activities will be retrieved.</param>
        /// <param name="page">Number of page that will be loaded</param>
        /// <param name="pageSize">Size of page which will be loaded.</param>
        /// <returns>ActivityForActivitiesListToResponse</returns>
        Task<List<ActivityForActivitiesListToResponse>> GetActivitiesPaged(string userId, int page, int pageSize);

        /// <summary>
        /// Retrieves all activities.
        /// </summary>
        /// <param name="page">Number of page that will be loaded</param>
        /// <param name="pageSize">Size of page which will be loaded.</param>
        /// <returns>ActivityForActivitiesListToResponse</returns>
        Task<List<ActivityForActivitiesListToResponse>> GetAllActivities(int page, int pageSize);

        /// <summary>
        /// Retrieves all activities of user.
        /// </summary>
        /// <param name="userId">User id which activities will be retrieved.</param>
        /// <param name="page">Number of page that will be loaded</param>
        /// <param name="pageSize">Size of page which will be loaded.</param>
        /// <param name="predicate">LINQ expression to filter activities that will be retrieved.</param>
        /// <returns>ActivityForActivitiesListToResponse</returns>
        Task<List<ActivityForActivitiesListToResponse>> GetFilteredActivitiesPaged(string userId, int page, int pageSize, Expression<Func<Activity, bool>> predicate);

        /// <summary>
        /// Retrieves all activities of user.
        /// </summary>
        /// <param name="userId">User id which recommended activities will be retrieved.</param>
        /// <param name="page">Number of page that will be loaded</param>
        /// <param name="pageSize">Size of page which will be loaded.</param>
        /// <returns>ActivityForActivitiesListToResponse</returns>
        Task<List<ActivityForActivitiesListToResponse>> GetRecommendedActivitiesPaged(string userId, int page, int pageSize);

        /// <summary>
        /// Retrieves all activities of user.
        /// </summary>
        /// <param name="page">Number of page that will be loaded</param>
        /// <param name="pageSize">Size of page which will be loaded.</param>
        /// <param name="predicate">LINQ expression to filter activities that will be retrieved.</param>
        /// <returns>ActivityForActivitiesListToResponse</returns>
        Task<List<ActivityForActivitiesListToResponse>> GetFilteredRecommendedActivitiesPaged(int page, int pageSize, Expression<Func<Activity, bool>> predicate);

        /// <summary>
        /// Retrieves activity by id.
        /// </summary>
        /// <param name="id">Id of activity which will be retrieved.</param>
        /// <returns>ActivityToResponse object with data of activity.</returns>
        Task<ActivityToResponse> GetActivity(string id);

        /// <summary>
        /// Deletes activity from the system.
        /// </summary>
        /// <param name="activityId">Activity id which will be deleted.</param>
        /// <param name="userId">User id which created the activity.</param>
        /// <returns></returns>
        Task DeleteActivity(string activityId, string userId);
    }
}
