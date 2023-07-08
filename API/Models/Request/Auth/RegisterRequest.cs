using API.Mappings;
using Domain.Constants;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Auth;

public class RegisterRequest : IMapTo<User>
{
    [Required]
    public string Name { get; set; }

    [Required, Phone]

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public Role Role { get; set; }
}
