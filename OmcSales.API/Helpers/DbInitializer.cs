using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using OmcSales.API.Models;

namespace OmcSales.API.Helpers
{
    public static class DbInitializer
    {

        public static void Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,ApplicationDbContext dbContext)
        {
            //I'm bombing here

            var roles = new string[] { "admin","manager"};

            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                {
                    IdentityRole identityRole = new IdentityRole();
                    identityRole.Name = role;
                    var m = roleManager.CreateAsync(identityRole).Result;
                }

            }

            var products = new string[] { "AGO", "PMS","KERO","GAS"};

            foreach(var product in products)
            {
                if(dbContext.ProductBank.Where(i => i.ProductName == product).Count() < 1)
                {
                    dbContext.ProductBank.Add(new ProductBank
                    {
                        ProductName = product
                    });
                }
            }

            dbContext.SaveChanges();
        }

    }
}
