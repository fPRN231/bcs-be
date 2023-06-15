using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class MedicalHistory
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string MedicalHistoryId { get; set; }
        [Required]
        public string MedicalHistoryName { get; set;}
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        [ForeignKey("Bird")]
        public string BirdId { get; set; }
        public virtual Bird Bird { get; set; }
    }
}
