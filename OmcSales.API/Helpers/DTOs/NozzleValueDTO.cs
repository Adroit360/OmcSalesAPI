using System;
namespace OmcSales.API.Helpers.DTOs
{
    public class NozzleValueDTO
    {
        public int NozzleValueId { get; set; }
        public int Opening { get; set; }
        public int Closing { get; set; }
        public DateTime Date { get; set; }
        public int NozzleId { get; set; }
    }
}
