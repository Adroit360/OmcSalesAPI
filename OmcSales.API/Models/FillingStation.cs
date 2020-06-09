using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OilBackend.Models
{
    public class FillingStation
    {
        public int FillingStationId { get; set; }

        public FillingStation()
        {
            Products = new List<Product>();
            Pumps = new List<Pump>();
        }
        public string Name { get; set; }
        public string Location { get; set; }


        public List<Product> Products { get; set; }
        public List<Pump> Pumps { get; set; }
    }
}
    