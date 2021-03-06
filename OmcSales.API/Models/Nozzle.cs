﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmcSales.API.Models
{
    public class Nozzle
    {
        public Nozzle()
        {
            NozzleValues = new List<NozzleValue>();
        }
        public int NozzleId { get; set; }

        public string NozzleName { get; set; }

        public int ProductId { get; set; }

        public int PumpId { get; set; }
        [ForeignKey("PumpId")]
        public Pump Pump { get; set; }

        public List<NozzleValue> NozzleValues { get; set; }


    }
}
