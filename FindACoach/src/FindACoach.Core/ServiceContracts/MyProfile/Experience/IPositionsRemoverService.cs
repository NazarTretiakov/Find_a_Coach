namespace FindACoach.Core.ServiceContracts.Forum.Activities
{
    /// <summary>
    /// Represents the service for deleting activities.
    /// </summary>
    public interface IPositionsRemoverService
    {
        /// <summary>
        /// Deletes position from the system.
        /// </summary>
        /// <param name="positionId">Position id which will be deleted.</param>
        /// <returns></returns>
        Task DeletePosition(string positionId);
    }
}
