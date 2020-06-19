using System;
using System.Collections.Generic;
using OmcSales.API.Models;

namespace OmcSales.API.Helpers.DTOs
{
    public class FillingStationDTO
    {
        public FillingStationDTO()
        {
            
        }
        public int FillingStationId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ManagerEmail { get; set; }

        public List<PumpDTO> Pumps { get; set; }

        public string UserId { get; set; }
    }
}
