using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OilBackend.Models
{
    public class Product
    {
        public Product()
        {
            ProductPrices = new List<ProductPrice>();
        }
       
        public int ProductId { get; set; }
        public int ProductName { get; set; }

        public decimal Price { get; set; }

        public int StationId { get; set; }

        [ForeignKey("StationId")]
        public FillingStation Station { get; set; }

        public List<ProductPrice> ProductPrices { get; set; }
    }
}
