using FindACoach.Core.DTO.Forum;

namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents service for toggling like for activity.
    /// </summary>
    public interface IToggleLikeService
    {
        /// <summary>
        /// Toggles users like for activity.
        /// </summary>
        /// <param name="dto">Data Transfer Object with data for toggling like.</param>
        /// <returns></returns>
        Task<LikesInformationToResponse> ToggleLike(ToggleLikeDTO dto);
    }
}
