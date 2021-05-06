using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SNSW_API.Models
{
    public class Insurer
    {
        [Key]
        public string Name { get; set; }
        public int Code { get; set; }
    }
}
