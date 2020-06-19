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

        /// <summary>
        /// Role can be directly applied here cos the user can only be in one
        /// role at a time .. either an admin or manager
        /// </summary>
        public string Role { get; set; }
        //String Array of products from product bank
        public string ProductsSold { get; set; }
    }
}
