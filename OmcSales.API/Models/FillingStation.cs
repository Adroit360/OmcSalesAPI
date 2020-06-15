﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmcSales.API.Models
{
    public class FillingStation
    {
        public int FillingStationId { get; set; }

        public FillingStation()
        {
            ProductStations = new List<ProductStation>();
            Pumps = new List<Pump>();
        }
        public string Name { get; set; }
        public string Location { get; set; }


        public List<ProductStation> ProductStations { get; set; }
        public List<Pump> Pumps { get; set; }
    }
}
    