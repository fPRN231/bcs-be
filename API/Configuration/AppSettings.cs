namespace API.Configuration;

public class AppSettings
{
    public DbConfig ConnectionStrings { get; set; }
    public JWTOptions JWTOptions { get; set; }
}
