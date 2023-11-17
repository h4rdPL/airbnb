using airbnb.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace airbnb.Application.Common.Interfaces
{
    public interface ICookieService
    {
        Task OnGetAsync();
        void SetUserCookie(User user);
    }
}
