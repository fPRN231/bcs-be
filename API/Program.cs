using API.Configuration;
using API.Utils;
using Domain.Application.AppConfig;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
{
    services.AddControllers(options =>
    {
        options.Filters.Add<ValidateModelStateFilter>();
    });
    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    services.AddEndpointsApiExplorer();
    services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
    services.AddBcsDbContext();
    services.AddJwtService();
    services.AddSwagger();
    services.AddCors(options => {
        options.AddPolicy("CorsPolicy", builder => {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });
}

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.Services.ApplyMigrations();
}
app.UseCors("CorsPolicy");
app.UseAutoWrapper();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
