using airbnb.Application.Common.Interfaces;
using airbnb.Infrastructure.Persistence;
using airbnb.Infrastructure.Rooms;
using Microsoft.Extensions.DependencyInjection;

namespace airbnb.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();


            return services;
        }

    }
}
