using API.Mappings;
using Domain.Constants;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Auth;

public class RegisterRequest : IMapTo<User>
{
    [Required, MinLength(3), MaxLength(32)]
    public string Username { get; set; }

    [Required, MinLength(3), MaxLength(32)]
    public string Password { get; set; }

    [Required, MinLength(3), MaxLength(32)]
    public string ReEnteredPassword { get; set; }

    [Required, Range(1, 4)]
    public Role Role { get; set; }
}
