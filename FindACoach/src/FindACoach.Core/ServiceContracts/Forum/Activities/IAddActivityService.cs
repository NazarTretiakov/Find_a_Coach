using FindACoach.Core.DTO.MyProfile;
using FindACoach.Core.DTO.MyProfile.Activities;

namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents the service for adding activity.
    /// </summary>
    public interface IAddActivityService
    {
        /// <summary>
        /// Adds activity of the user.
        /// </summary>
        /// <param name="userId">User Id which activity will be added.</param>
        /// <param name="dto">Data Transfer Object with data to add activity.</param>
        /// <returns></returns>
        Task AddActivity(string userId, AddActivityDTO dto);
    }
}
