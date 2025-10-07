using FindACoach.Core.DTO.Forum;
using FindACoach.Core.DTO.MyProfile.Activities;

namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents the service for retrieving comments from system.
    /// </summary>
    public interface ICommentsGetterService
    {
        /// <summary>
        /// Retrieves paged comments of activity.
        /// </summary>
        /// <param name="activityId">Activity id which comments will be retrieved.</param>
        /// <param name="page">Number of page that will be loaded</param>
        /// <param name="pageSize">Size of page which will be loaded.</param>
        /// <returns></returns>
        Task<List<CommentToResponse>> GetCommentsPaged(string activityId, int page, int pageSize);
    }
}
