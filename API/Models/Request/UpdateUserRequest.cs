using API.Mappings;
using Domain.Constants;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request
{
    public class UpdateUserRequest : IMapTo<User>
    {     
        public string Name { get; set; }

        public string Username { get; set; }

        public string PhoneNumber { get; set; }

        
        public string Address { get; set; }

        
        public string Email { get; set; }

        
        public string Password { get; set; }

        public virtual Role Role { get; set; }
    }
}
