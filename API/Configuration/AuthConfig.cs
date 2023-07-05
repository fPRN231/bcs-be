using Domain.Application.AppConfig;
using Domain.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Configuration;

public static class AuthConfig
{
    public static IServiceCollection AddJwtService(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var appSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();
                var jwtOptions = appSettings.Value.JWTOptions;
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = jwtOptions.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOptions.IssuerSigningKey)),
                    ValidateIssuer = jwtOptions.ValidateIssuer,
                    ValidIssuer = jwtOptions.ValidIssuer,
                    ValidateAudience = jwtOptions.ValidateAudience,
                    ValidAudience = jwtOptions.ValidAudience,
                    RequireExpirationTime = jwtOptions.RequireExpirationTime,
                    ValidateLifetime = jwtOptions.RequireExpirationTime,
                    ClockSkew = TimeSpan.FromDays(1),
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[Constants.ACCESS_TOKEN];
                        return Task.CompletedTask;
                    }
                };
            });
        return services;
    }

    public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options => {
            options.AddPolicy(PolicyName.ADMIN,policy => policy.RequireRole(PolicyName.ADMIN));
            options.AddPolicy(PolicyName.CUSTOMER, policy => policy.RequireRole(PolicyName.CUSTOMER));
            options.AddPolicy(PolicyName.DOCTOR, policy => policy.RequireRole(PolicyName.DOCTOR));
            options.AddPolicy(PolicyName.STAFF, policy => policy.RequireRole(PolicyName.STAFF));
        });
        return services;
    }
}
