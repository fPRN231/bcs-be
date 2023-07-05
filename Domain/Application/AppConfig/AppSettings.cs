namespace Domain.Application.AppConfig;

public class AppSettings
{
    public DbConfig ConnectionStrings { get; set; }
    public JWTOptions JWTOptions { get; set; }
    public string SpaUrl { get; set; }
}
