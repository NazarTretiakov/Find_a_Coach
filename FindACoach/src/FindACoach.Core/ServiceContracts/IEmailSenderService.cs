namespace FindACoach.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for sending email.
    /// </summary>
    public interface IEmailSenderService
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="email">Email address to which email will be sent.</param>
        /// <param name="subject">Subject of the email.</param>
        /// <param name="message">Content of the email.</param>
        /// <returns></returns>
        Task SendEmailAsync(string email, string subject, string message);
    }
}
