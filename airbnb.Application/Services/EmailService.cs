using airbnb.Application.Common.Interfaces;
using System.Text.RegularExpressions;

namespace airbnb.Application.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> IsValidEmail(string email)
        {
            const string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
