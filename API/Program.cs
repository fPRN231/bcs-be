using Api;
using API.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.Context;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;  

services.AddControllers(options => {
    options.Filters.Add<ValidateModelStateFilter>();
});
services.AddEndpointsApiExplorer();
services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
services.AddDbContext<BCSManagementContext>(options => {
    var appSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>().Value;
    Console.WriteLine(appSettings);
    options.UseSqlServer(appSettings.ConnectionStrings.BCSManagementDB);
});

services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
