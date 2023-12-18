using airbnb.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace airbnb.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Validates whether the provided email address is in a valid format.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>Returns true if the email is valid; otherwise, false.</returns>

        public async Task<bool> IsValidEmail(string email)
        {
            const string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            return Regex.IsMatch(email, emailPattern);
        }

        /// <summary>
        /// Sends an email asynchronously using the configured SMTP server.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="message">The content of the email.</param>
        /// <returns>Returns a task representing the asynchronous operation.</returns>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "airbnb_01@outlook.com";
            var password = _config["Email:ServicePassword"];
            

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };

            return client.SendMailAsync(
                new MailMessage(
                    from: mail,
                    to: email,
                    subject,
                    message
                    ));
        }
    }
}
