using FindACoach.Core.DTO.MyProfile;

namespace FindACoach.Core.ServiceContracts.CompleteProfileWindow
{
    /// <summary>
    /// Represents the service for getting the state of the "Complete profile" window.
    /// </summary>
    public interface IGetStateService
    {
        /// <summary>
        /// Gets state (visibility and title) of "Complete profile" window
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>"Complete profile" window DTO object.</returns>
        Task<CompleteProfileWindowStateDTO> GetState(string userId);
    }
}

