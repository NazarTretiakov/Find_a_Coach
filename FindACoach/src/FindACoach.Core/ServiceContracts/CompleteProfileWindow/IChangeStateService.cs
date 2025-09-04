namespace FindACoach.Core.ServiceContracts.CompleteProfileWindow
{
    /// <summary>
    /// Represents the service for changing the state of the "Complete profile" window.
    /// </summary>
    public interface IChangeStateService
    {
        /// <summary>
        /// Changes state (visibility and title) of "Complete profile" window
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="isVisible"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        Task ChangeState(string userId, bool isVisible, string title);
    }
}
