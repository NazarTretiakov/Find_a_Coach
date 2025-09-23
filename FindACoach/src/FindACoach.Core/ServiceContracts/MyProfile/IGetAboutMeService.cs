using FindACoach.Core.DTO.MyProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindACoach.Core.ServiceContracts.MyProfile
{
    /// <summary>
    /// Represents the service for getting "About me" information.
    /// </summary>
    public interface IGetAboutMeService
    {
        /// <summary>
        /// Retrieves "About me" information of user.
        /// </summary>
        /// <param name="userId">User id which information will be retrieved.</param>
        /// <returns>AboutMeDTO object with information.</returns>
        Task<AboutMeDTO> GetAboutMe(string userId);
    }
}
