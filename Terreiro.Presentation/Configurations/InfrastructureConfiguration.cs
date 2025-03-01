﻿using Microsoft.EntityFrameworkCore;
using Terreiro.Application.Repositories;
using Terreiro.Persistence.Data;
using Terreiro.Persistence.Repositories;

namespace Terreiro.Presentation.Configuration;

public static class InfrastructureConfiguration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TerreiroDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default"))
        );

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IEventItemRepository, EventItemRepository>();
        services.AddScoped<IUserEventRepository, UserEventRepository>();
        services.AddScoped<IUserEventItemRepository, UserEventItemRepository>();
    }
}
