using airbnb.Application.Common.Interfaces;
using airbnb.Application.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace airbnb.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
