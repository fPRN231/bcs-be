using API.Configuration;
using API.Utils;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;  

services.AddControllers(options => {
    options.Filters.Add<ValidateModelStateFilter>();
});
services.AddEndpointsApiExplorer();
services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
services.AddBcsDbContext();

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
