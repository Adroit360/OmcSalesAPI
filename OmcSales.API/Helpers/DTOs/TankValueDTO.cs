using System;
namespace OmcSales.API.Helpers.DTOs
{
    public class TankValueDTO
    {
        public int TankValueId { get; set; }

        public int TankId { get; set; }
        public float Opening { get; set; }
        public float Closing { get; set; }
        public float Deliveredproduct { get; set; }
        public float RTT { get; set; }

        public DateTime Date { get; set; }
    }
}
