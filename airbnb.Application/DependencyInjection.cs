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

            // every time we send email => every time instance is created 
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<ICookieService, CookieService>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
