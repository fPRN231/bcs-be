using API.Mappings;
using Domain.Constants;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Response;

public class RegisterResponse : IMapFrom<User>
{
    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    public Role Role { get; set; }
}
