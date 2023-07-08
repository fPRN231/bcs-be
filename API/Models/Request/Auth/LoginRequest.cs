using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Auth;

public class LoginRequest
{
    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
