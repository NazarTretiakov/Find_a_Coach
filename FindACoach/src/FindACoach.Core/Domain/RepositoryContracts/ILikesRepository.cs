using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.DTO.Forum;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Like entity.
    /// </summary>
    public interface ILikesRepository
    {
        /// <summary>
        /// Retrieves like from the data store.
        /// </summary>
        /// <param name="userId">The id of user which liked the activity.</param>
        /// <param name="activityId">The id of activity which was liked.</param>
        /// <returns>Like object or null.</returns>
        Task<Like> GetLike(string userId, string activityId);

        /// <summary>
        /// Adds like to the data store.
        /// </summary>
        /// <param name="activityId">Activity id which was liked.</param>
        /// <param name="userId">User id that liked a activity.</param>
        /// <returns>True if like was added. Otherwise false.</returns>
        Task<LikesInformationToResponse> AddLike(string userId, string activityId);

        /// <summary>
        /// Removes like from the data store.
        /// </summary>
        /// <param name="like">Like object which will be removed.</param>
        /// <returns>True if like was removed. Otherwise false.</returns>
        Task<LikesInformationToResponse> RemoveLike(Like like);

        /// <summary>
        /// Checks if the activity is liked by the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="activityId"></param>
        /// <returns>True - if activity is liked, otherwise false.</returns>
        Task<bool> IsActivityLikedByUser(string userId, string activityId);
    }
}
