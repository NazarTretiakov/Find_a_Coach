using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.DTO.Forum;

namespace FindACoach.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Save entity.
    /// </summary>
    public interface ISavesRepository
    {
        /// <summary>
        /// Retrieves save from the data store.
        /// </summary>
        /// <param name="userId">The id of user which saved the activity.</param>
        /// <param name="activityId">The id of activity which was saved.</param>
        /// <returns>Save object or null.</returns>
        Task<Save> GetSave(string userId, string activityId);

        /// <summary>
        /// Adds save to the data store.
        /// </summary>
        /// <param name="activityId">Activity id which was saved.</param>
        /// <param name="userId">User id that saved a activity.</param>
        /// <returns>True if save was added. Otherwise false.</returns>
        Task<SaveInformationToResponse> AddSave(string userId, string activityId);

        /// <summary>
        /// Removes save from the data store.
        /// </summary>
        /// <param name="save">Save object which will be removed.</param>
        /// <returns>True if save was removed. Otherwise false.</returns>
        Task<SaveInformationToResponse> RemoveSave(Save save);

        /// <summary>
        /// Checks if the activity is saved by the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="activityId"></param>
        /// <returns>True - if activity is saved, otherwise false.</returns>
        Task<bool> IsActivitySavedByUser(string userId, string activityId);
    }
}
