using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OmcSales.API.Models;

namespace OmcSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelperController : ControllerBase
    {
        public ApplicationDbContext DbContext { get; set; }
        public HelperController(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet("productsfrombank")]
        public ActionResult<List<ProductBank>> GetProductsFromBank()
        {
            return DbContext.ProductBanks.ToList();
        }
    }
}