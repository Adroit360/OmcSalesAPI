using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmcSales.API.Helpers.DTOs;
using OmcSales.API.Models;

namespace OmcSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ApplicationDbContext DbContext { get; set; }
        public IMapper Mapper { get; set; }
        public ProductsController(ApplicationDbContext dbContext,IMapper mapper)
        {
            DbContext = dbContext;
            Mapper =  mapper;
        }

        [HttpGet("foruser/{userId}")]
        public async Task<ActionResult<IEnumerable<ProductForReturnDTO>>> GetUserProducts(string userId)
        {
            var productsForUser = DbContext.Products.Where(i => i.UserId == userId);

            var productsForReturn = Mapper.Map<List<ProductForReturnDTO>>(productsForUser);

            return productsForReturn;
        }

        [HttpPost("changeprice")]
        public async Task<ActionResult<ProductForReturnDTO>> ChangeProductPrice(ProductPriceChangeDTO productPriceChangeDTO)
        {
            var product = await DbContext.Products.FindAsync(productPriceChangeDTO.ProductId);

            if (product == null){
                return BadRequest("Invalid Product");
            }
            
            DbContext.ProductPrices.Add(new ProductPrice
            {
                Date = DateTime.Now,
                Price = productPriceChangeDTO.NewPrice,
                ProductId = productPriceChangeDTO.ProductId
            });

            product.Price = productPriceChangeDTO.NewPrice;

            DbContext.Entry(product).State = EntityState.Modified;

            await DbContext.SaveChangesAsync();

            var productToReturn = Mapper.Map<ProductForReturnDTO>(product);

            return Ok(productToReturn);
        }

        [HttpGet("getprices/{productId}")]
        public ActionResult<ProductPrice> GetProductPrices(int productId) {
            var productPrices = DbContext.ProductPrices.Where(i => i.ProductId == productId);
            return Ok(productPrices);
        }

    }

    public class ProductPriceChangeDTO
    {
        public int ProductId { get; set; }

        public decimal NewPrice { get; set; }
    }
}