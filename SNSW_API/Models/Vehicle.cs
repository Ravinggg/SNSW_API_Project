using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SNSW_API.Models
{
    public class Vehicle
    {
        [Key]
        public string Type { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public string Vin { get; set; }
        public int? Tare_weight { get; set; }
        public int? Gross_mass { get; set; }
    }

}