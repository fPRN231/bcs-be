using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Repository.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Add services to the container.
builder.Services.AddDbContext<BCSManagementContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BCSManagement")));

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
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
