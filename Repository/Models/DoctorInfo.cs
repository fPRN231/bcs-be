﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Persistence.Models;

namespace Repository.Models
{
    public partial class DoctorInfo : BaseEntity
    {
        [Required]
        public string UserId { get; set; }

        public int YearsOfExperience { get; set; }

        public int Rating { get; set; }

        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<Qualification> Qualifications { get; set; }
    }
}