using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OilBackend.Models
{
    public class NozzleValue { 
    
        //Suggesting we use string for these values...
        public int NozzleValueId { get; set; }
        public int opening { get; set; }
        public int closing { get; set; }
        public DateTime Date { get; set; }
        public int NozzleId { get; set; }
        [ForeignKey("NozzleId")]
        public Nozzle Nozzle { get; set; }
    }
}
