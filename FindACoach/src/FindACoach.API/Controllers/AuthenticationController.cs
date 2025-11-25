using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.DTO.Authentication;
using FindACoach.Core.Enums;
using FindACoach.Core.ServiceContracts;
using FindACoach.Core.ServiceContracts.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;

namespace FindACoach.API.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : CustomControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJWTService _jwtService;
        private readonly IEmailSenderService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager, IJWTService jwtService, IEmailSenderService emailService, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _emailService = emailService;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterDTO registerDTO)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(new { errorMessage = errorMessage });
            }

            User user = new User()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                EmailConfirmed = false,
                IsCompleteProfileWindowVisible = true,
                CompleteProfileWindowTitle = "You have successfully created the account",
                ImagePath = "default-profile-image.png"
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRoleOptions.User.ToString());

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                var confirmEmailUrl = Url.ActionLink(
                    action: "ConfirmEmail",
                    controller: "Authentication",
                    values: new { userId = user.Id, token = token },
                    protocol: Request.Scheme);

                await SendConfirmationEmail(user, confirmEmailUrl);

                return Ok(new { message = "Registration completed successfully. Please check your email to confirm your account." });
            }
            else
            {
                string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                return Problem(errorMessage);
            }
        }

        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromQuery] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || user.EmailConfirmed)
            {
                return Ok(new { message = "If the account exists, an email with instructions has been sent to the specified email." });
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var confirmEmailUrl = Url.ActionLink(
                action: "ConfirmEmail",
                controller: "Authentication",
                values: new { userId = user.Id, token = token },
                protocol: Request.Scheme);

            await SendConfirmationEmail(user, confirmEmailUrl);

            return Ok(new { message = "Confirmation email has been sent. Please check your email." });
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest(new { errorMessage = "Invalid parameters for email confirmation." });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(new { errorMessage = $"User with ID \"{userId}\" not found." });
            }

            byte[] decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
            {
                var authenticationResponse = await _jwtService.CreateJwtToken(user);

                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTime;
                await _userManager.UpdateAsync(user);

                await _signInManager.SignInAsync(user, isPersistent: false);

                string clientUrl = _configuration.GetValue<string>("ClientUrl");

                string redirectUrl = $"{clientUrl}/register/email-confirmed?" +
                    $"&email={Uri.EscapeDataString(user.Email)}" +
                    $"&role={Uri.EscapeDataString(authenticationResponse.Role)}" +
                    $"&token={Uri.EscapeDataString(authenticationResponse.Token)}" +
                    $"&expiration={Uri.EscapeDataString(authenticationResponse.Expiration.ToString("o"))}" +
                    $"&refreshToken={Uri.EscapeDataString(authenticationResponse.RefreshToken)}" +
                    $"&refreshTokenExpiration={Uri.EscapeDataString(authenticationResponse.RefreshTokenExpirationDateTime.ToString("o"))}" +
                    $"&isCompleteProfileWindowVisible={Uri.EscapeDataString(user.IsCompleteProfileWindowVisible.ToString())}" + 
                    $"&completeProfileWindowTitle={Uri.EscapeDataString(user.CompleteProfileWindowTitle)}";

                return Redirect(redirectUrl);
            }
            else
            {
                return BadRequest(new { errorMessage = "Error confirming your email. The link is invalid or expired." });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(new { errorMessage = errorMessage });
            }

            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                return BadRequest(new { errorMessage = "Invalid email or password."});
            }

            if (!user.EmailConfirmed)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                var confirmEmailUrl = Url.ActionLink(
                    action: "ConfirmEmail",
                    controller: "Authentication",
                    values: new { userId = user.Id, token = token },
                    protocol: Request.Scheme);

                await SendConfirmationEmail(user, confirmEmailUrl);

                return BadRequest(new { errorMessage = "You need to confirm your email before logging in." });
            }

            if (user.IsBlocked == true)
            {
                return BadRequest(new { errorMessage = "Your account has been blocked." });
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var authenticationResponse = await _jwtService.CreateJwtToken(user);

                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTime;
                await _userManager.UpdateAsync(user);
                if (user.IsLoginNotificationEnabled)
                {
                    await SendLoginNotificationEmail(user);
                }

                return Ok(authenticationResponse);
            }
            else
            {
                return BadRequest(new { errorMessage = "Invalid email or password" });
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                return Ok(new { message = "If an account with this email exists, a password reset link has been sent." });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            string clientUrl = _configuration.GetValue<string>("ClientUrl");
            string resetPasswordUrl = $"{clientUrl}/reset-password?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";

            await SendResetPasswordEmail(user, resetPasswordUrl);

            return Ok(new { message = "If an account with this email exists, a password reset link has been sent." });
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);
            if (user == null)
            {
                return BadRequest(new { errorMessage = "User not found." });
            }

            var decodedToken = WebEncoders.Base64UrlDecode(resetPasswordDTO.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, resetPasswordDTO.NewPassword);

            if (result.Succeeded)
            {
                var authenticationResponse = await _jwtService.CreateJwtToken(user);

                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTime;

                await _userManager.UpdateAsync(user);
                await _signInManager.SignInAsync(user, isPersistent: false);

                return Ok(authenticationResponse);
            }
            else
            {
                return BadRequest(new { errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user != null)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.SignOutAsync();

            return NoContent();
        }

        [HttpPost("refresh-jwt-token")]
        public async Task<IActionResult> RefreshJwtToken([FromBody] TokenModel tokenModel)
        {
            if (tokenModel == null)
            {
                return BadRequest(new { errorMessage = "Invalid parameters for refreshing of JWT Token." });
            }

            ClaimsPrincipal? principal = _jwtService.GetPrincipalFromJwtToken(tokenModel.JwtToken);

            if (principal == null)
            {
                return BadRequest(new { errorMessage = "Invalid jwt access token" });
            }

            string? email = principal.FindFirstValue(ClaimTypes.Email);

            User? user = await _userManager.FindByEmailAsync(email);

            if (user == null || user.RefreshToken != tokenModel.RefreshToken)
            {
                return BadRequest();
            }

            if (user.RefreshTokenExpirationDateTime <= DateTime.Now)
            {
                return BadRequest(new { errorMessage = "Session has expired. Please log in again." });
            }

            AuthenticationResponse authenticationResponse = await _jwtService.CreateJwtToken(user);

            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTime;

            await _userManager.UpdateAsync(user);

            return Ok(authenticationResponse);
        }

        private async Task SendConfirmationEmail(User user, string confirmEmailUrl)
        {
            var templatePath = Path.Combine(_hostEnvironment.ContentRootPath, "..", "FindACoach.Core", "EmailTemplates", "ConfirmEmailTemplate.html");
            var confirmEmailTemplate = await System.IO.File.ReadAllTextAsync(templatePath);

            string clientUrl = _configuration.GetValue<string>("ClientUrl");

            confirmEmailTemplate = confirmEmailTemplate.Replace("@clientUrl", clientUrl);
            confirmEmailTemplate = confirmEmailTemplate.Replace("@confirmEmailUrl", confirmEmailUrl);

            await _emailService.SendEmailAsync(user.Email, "Confirm your email in Find a Coach", confirmEmailTemplate);
        }

        private async Task SendResetPasswordEmail(User user, string resetPasswordUrl)
        {
            var templatePath = Path.Combine(_hostEnvironment.ContentRootPath, "..", "FindACoach.Core", "EmailTemplates", "ResetPasswordTemplate.html");
            var confirmEmailTemplate = await System.IO.File.ReadAllTextAsync(templatePath);

            string clientUrl = _configuration.GetValue<string>("ClientUrl");

            confirmEmailTemplate = confirmEmailTemplate.Replace("@clientUrl", clientUrl);
            confirmEmailTemplate = confirmEmailTemplate.Replace("@resetPasswordUrl" +
                "", resetPasswordUrl);

            await _emailService.SendEmailAsync(user.Email, "Reset your password in Find a Coach", confirmEmailTemplate);
        }

        private async Task SendLoginNotificationEmail(User user)
        {
            var templatePath = Path.Combine(_hostEnvironment.ContentRootPath, "..", "FindACoach.Core", "EmailTemplates", "LoginNotificationTemplate.html");
            var loginNotificationTemplate = await System.IO.File.ReadAllTextAsync(templatePath);

            string clientUrl = _configuration.GetValue<string>("ClientUrl");

            loginNotificationTemplate = loginNotificationTemplate.Replace("@clientUrl", clientUrl);

            await _emailService.SendEmailAsync(user.Email, "New login to your Find a Coach account", loginNotificationTemplate);
        }
    }
}