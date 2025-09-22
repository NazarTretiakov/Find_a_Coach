using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.DTO.Authentication;
using System.Security.Claims;

namespace FindACoach.Core.ServiceContracts.Authentication
{
    /// <summary>
    /// Represents the service for manipulating the JWT token.
    /// </summary>
    public interface IJWTService
    {
        /// <summary>
        /// Creates the JWT token for the supplied user.
        /// </summary>
        /// <param name="user">User for which will be created JWT token.</param>
        /// <returns>AuthenticationResponse object with the generated token.</returns>
        Task<AuthenticationResponse> CreateJwtToken(User user);

        /// <summary>
        /// Retrives the principal from Jwt token.
        /// </summary>
        /// <param name="token">JWT token of the user.</param>
        /// <returns>ClaimsPrincipal object with the user's data.</returns>
        ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
    }
}
