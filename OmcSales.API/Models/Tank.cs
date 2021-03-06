﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmcSales.API.Models
{
    public class Tank
    {
        public Tank()
        {
            Tankvalues = new List<TankValue>();
        }
        public int TankId { get; set; }
        public string TankName { get; set; }
        public int ProductId { get; set; }
        public int StationId { get; set; }
        //This is a foreign Key, check the data type
        public List<TankValue> Tankvalues { get; set; }
    }
}
