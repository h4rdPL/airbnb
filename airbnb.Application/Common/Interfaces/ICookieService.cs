using airbnb.Domain.Models;

namespace airbnb.Application.Common.Interfaces
{
    public interface ICookieService
    {
        Task OnGetAsync();
        Task SetUserCookie(User user);
    }
}
