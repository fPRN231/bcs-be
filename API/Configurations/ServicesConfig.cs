﻿using Domain.Application.AppConfig;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Persistence.Repositories;
using Repository.Context;

namespace API.Configurations;

public static class ServicesConfig
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BCS Management",
                Version = "v1"
            });
            OpenApiSecurityScheme securityDefinition = new()
            {
                Name = "Bearer",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Description = "Specify the authorization token.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
            };
            c.AddSecurityDefinition("jwt_auth", securityDefinition);
            OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Id = "jwt_auth",
                    Type = ReferenceType.SecurityScheme
                }
            };
            OpenApiSecurityRequirement securityRequirements = new() {
            {
                securityScheme,
                new string[] { }
            },
        };
            c.AddSecurityRequirement(securityRequirements);
        });
        return services;
    }

    public static IServiceCollection AddBcsDbContext(this IServiceCollection services)
    {
        services.AddDbContext<BCSManagementContext>(options =>
        {
            var appSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>().Value;
            options.UseSqlServer(appSettings.ConnectionStrings.BCSManagementDB);
        });
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        return services;
    }

    public static async Task ApplyMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        await using BCSManagementContext dbContext =
            scope.ServiceProvider.GetRequiredService<BCSManagementContext>();
        await dbContext.Database.MigrateAsync();
    }
}
