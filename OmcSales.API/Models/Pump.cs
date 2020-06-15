using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OilBackend.Models
{
    public class Pump
    {
        public Pump()
        {
            Nozzles = new List<Nozzle>();
        }

        public int PumpId { get; set; }
        public string AttendantName { get; set; }

        public int StationId { get; set; }
        [ForeignKey("StationId")]
        public FillingStation FillingStation { get; set; }

        public List<Nozzle> Nozzles { get; set; }
    }
}