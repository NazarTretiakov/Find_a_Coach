using FindACoach.Core.DTO.Forum;

namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents service for toggling save for activity.
    /// </summary>
    public interface IToggleSaveService
    {
        /// <summary>
        /// Toggles users save for activity.
        /// </summary>
        /// <param name="dto">Data Transfer Object with data for toggling save.</param>
        /// <returns></returns>
        Task<SaveInformationToResponse> ToggleSave(ToggleSaveDTO dto);
    }
}
