namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents the service for deleting comments.
    /// </summary>
    public interface ICommentsRemoverService
    {
        /// <summary>
        /// Deletes comment from the system.
        /// </summary>
        /// <param name="commentId">Comment id which will be deleted.</param>
        /// <param name="userId">User id which created the comment.</param>
        /// <returns></returns>
        Task DeleteComment(string commentId, string userId);
    }
}
