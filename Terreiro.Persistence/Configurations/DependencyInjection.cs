using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Terreiro.Application.Repositories;
using Terreiro.Persistence.Repositories;

namespace Terreiro.Persistence.Configurations;

public static class DependencyInjection
{
    public static void AddPersistenceDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IEventItemRepository, EventItemRepository>();
        services.AddScoped<IUserEventRepository, UserEventRepository>();
        services.AddScoped<IUserEventItemRepository, UserEventItemRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
    }
}
