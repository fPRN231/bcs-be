using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Persistence.Models;
using System.Text.Json.Serialization;

namespace Repository.Models
{
    public partial class Bird : BaseEntity
    {
        public Bird()
        {
            MedicalHistories = new HashSet<MedicalHistory>();
        }

        public string Name { get; set; }

        public string Species { get; set; }

        public int Age { get; set; }

        public bool Gender { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<MedicalHistory> MedicalHistories { get; set; }
    }
}