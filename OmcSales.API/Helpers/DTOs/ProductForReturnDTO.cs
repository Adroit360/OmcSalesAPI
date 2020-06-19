using System;
namespace OmcSales.API.Helpers.DTOs
{
    public class ProductForReturnDTO
    {
       
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }
    }
}
