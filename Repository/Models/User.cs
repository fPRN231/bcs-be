﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Persistence.Constants;
using Persistence.Models;

namespace Repository.Models
{
    public partial class User : BaseEntity
    {
        public User()
        {
            BirdsOwned = new HashSet<Bird>();
            DoctorLogTimes = new HashSet<DoctorLogTime>();
            Appointments = new HashSet<Appointment>();
        }

        public string Name { get; set; }
        
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public virtual Role Role { get; set; }

        public virtual DoctorInfo DoctorInfo { get; set; }

        [JsonIgnore]
        public virtual ICollection<Bird> BirdsOwned { get; set; }

        [JsonIgnore]
        public virtual ICollection<DoctorLogTime> DoctorLogTimes { get; set; }

        [JsonIgnore]
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
