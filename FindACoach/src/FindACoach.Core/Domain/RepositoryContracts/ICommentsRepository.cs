using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.DTO.MyProfile.Activities;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Comment entity.
    /// </summary>
    public interface ICommentsRepository
    {
        /// <summary>
        /// Adds comment to system.
        /// </summary>
        /// <param name="comment">Comment object that will be added to db.</param>
        /// <returns>CommentToResponse object with information of created comment.</returns>
        Task<CommentToResponse> AddComment(Comment comment);

        /// <summary>
        /// Retrieves paged comments of activity.
        /// </summary>
        /// <param name="activityId">Activity id which comments will be retrieved.</param>
        /// <param name="page">Number of page that will be loaded</param>
        /// <param name="pageSize">Size of page which will be loaded.</param>
        /// <returns></returns>
        Task<List<CommentToResponse>> GetCommentsPaged(string activityId, int page, int pageSize);

        /// <summary>
        /// Deletes comment from the system.
        /// </summary>
        /// <param name="commentId">Comment id which will be deleted.</param>
        /// <param name="userId">User id which created comment.</param>
        /// <returns></returns>
        Task DeleteComment(string commentId, string userId);
    }
}
