using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Auth;

public class LoginRequest
{
    [Required, MinLength(3), MaxLength(32)]
    public string Username { get; set; }

    [Required, MinLength(3), MaxLength(32)]
    public string Password { get; set; }
}
