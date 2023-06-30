using Api;
using API.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Repository.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;  

// Add services to the container.

services.AddControllers(options => {
    options.Filters.Add<ValidateModelStateFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
services.AddDbContext<BCSManagementContext>(options => {
    var appSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>().Value;
    Console.WriteLine(appSettings);
    options.UseSqlServer(appSettings.ConnectionStrings.BCSManagementDB);
});

services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
