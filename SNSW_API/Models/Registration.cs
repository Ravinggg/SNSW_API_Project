using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SNSW_API.Models
{
    public class Registration
    {
        [Key]
        public string Plate_number { get; set; }

        public ICollection<RegistrationDetail> RegistrationDetails { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Insurer> Insurers { get; set; }
    }
}
