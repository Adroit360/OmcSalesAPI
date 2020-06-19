using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmcSales.API.Models
{
    public class TankValue
    {
        public int TankValueId { get; set; }

        public int TankId { get; set; }
        [ForeignKey("TankId")]
        public Tank Tank { get; set; }

        public float Opening { get; set; }
        public float Closing { get; set; }
        public float Deliveredproduct { get; set; }
        public float RTT { get; set; }

        public DateTime Date { get; set; }
    }
}
