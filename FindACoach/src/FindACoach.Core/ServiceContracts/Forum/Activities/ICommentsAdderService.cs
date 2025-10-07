using FindACoach.Core.DTO.Forum;

namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents the service for creating new comments.
    /// </summary>

    public interface ICommentsAdderService
    {
        /// <summary>
        /// Adds a comment by current working user to the system.
        /// </summary>
        /// <param name="dto">The data transfer object that contains information about the comment that will be created.</param>
        /// <returns>CommentToResponse object which contains data of created comment.</returns>
        Task<CommentToResponse> CreateComment(AddCommentDTO dto);
    }
}
