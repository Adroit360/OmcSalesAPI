using System;
using System.Collections.Generic;
using OmcSales.API.Models;

namespace OmcSales.API.Helpers.DTOs
{
    public class StatisticsDTO
    {
        public Tank Tank { get; set; }

        public float FirstOpening { get; set; }

        public float LastClosing { get; set; }

        public float FuelPercentage { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
