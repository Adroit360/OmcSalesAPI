using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmcSales.API.Models
{
    public class ProductStation
    {
        public int ProductStationId { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int StationId { get; set; }
        [ForeignKey("StationId")]
        public FillingStation FillingStation { get; set; }
    }
}
