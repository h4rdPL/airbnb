using Microsoft.AspNetCore.Mvc;

namespace airbnb.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<bool> IsValidEmail(string email);
        Task SendEmailAsync(string email, string subject, string message);
    }
}
