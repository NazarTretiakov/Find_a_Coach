using FindACoach.Core.DTO.MyProfile;

namespace FindACoach.Core.ServiceContracts.MyProfile
{
    /// <summary>
    /// Represents the service for getting the information about is the profile sections of the active user is filled or not.
    /// </summary>
    public interface IIsProfileSectionsCompletedService
    {
        /// <summary>
        /// Determines whether all required sections of the user's profile are completed.
        /// </summary>
        /// <param name="userId">User id which profile sections will be checked on completion.</param>
        /// <returns>IsProfileSectionsFilledToResponse object with info about if the each section is filled or not.</returns>
        Task<IsProfileSectionsCompletedToResponse> IsProfileSectionsCompleted(string userId);
    }
}
