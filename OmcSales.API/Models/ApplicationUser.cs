using System;
using Microsoft.AspNetCore.Identity;

namespace OmcSales.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public string Name { get; set; }

        public string Token { get; set; }

        //String Array of products from product bank
        public string ProductsSold { get; set; }
    }
}
