using FindACoach.Core.DTO.MyProfile.Experience;

namespace FindACoach.Core.ServiceContracts.MyProfile.Experience
{
    /// <summary>
    /// Represents the service for editing positions.
    /// </summary>
    public interface IPositionsEditorService
    {
        /// <summary>
        /// Edits existing position.
        /// </summary>
        /// <param name="dto">Data Transfer Object with data of position to edit.</param>
        /// <returns></returns>
        Task EditPosition(EditPositionDTO dto);
    }
}
