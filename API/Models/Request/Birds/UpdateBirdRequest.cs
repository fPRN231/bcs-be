using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Birds
{
    public class UpdateBirdRequest : IMapTo<Bird>
    {
        public string Name { get; set; }

        public string Species { get; set; }

        public int Age { get; set; }

        public bool Gender { get; set; }

        public Guid UserId { get; set; }
    }
}
