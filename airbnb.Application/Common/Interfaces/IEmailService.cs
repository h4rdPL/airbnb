using Microsoft.AspNetCore.Mvc;

namespace airbnb.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<bool> IsValidEmail(string email);
    }
}
