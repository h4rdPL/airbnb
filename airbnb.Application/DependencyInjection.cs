using airbnb.Application.Common.Interfaces;
using airbnb.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace airbnb.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<ICookieService, CookieService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
