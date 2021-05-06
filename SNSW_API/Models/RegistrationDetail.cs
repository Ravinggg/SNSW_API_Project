using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SNSW_API.Models
{
    public class RegistrationDetail
    {
        [Key]
        public DateTime Expiry_date { get; set; }
        public Boolean Expired { get; set; }
    }

}
