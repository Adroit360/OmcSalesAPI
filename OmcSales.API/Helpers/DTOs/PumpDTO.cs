using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OmcSales.API.Models;

namespace OmcSales.API.Helpers.DTOs
{
    public class PumpDTO
    {
        public PumpDTO()
        {
        }

        public int PumpId { get; set; }
        public string PumpName { get; set; }
        public string AttendantName { get; set; }
        public int StationId { get; set; }

        public List<NozzleDTO> Nozzles { get; set; }
    }
}
