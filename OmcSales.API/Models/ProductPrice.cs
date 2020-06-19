using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmcSales.API.Models
{
    public class ProductPrice
    {

        public int ProductPriceId { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}