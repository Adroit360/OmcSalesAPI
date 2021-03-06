﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OmcSales.API.Models
{
    public class Product
    {
        public Product()
        {
            ProductPrices = new List<ProductPrice>();
        }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string UserId { get; set; }

        public List<ProductPrice> ProductPrices { get; set; }

    }
}
