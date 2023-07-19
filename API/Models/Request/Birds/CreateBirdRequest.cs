using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Birds
{
    public class CreateBirdRequest : IMapTo<Bird>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
