using System;
using System.Collections.Generic;
using OmcSales.API.Helpers.Enums;

namespace OmcSales.API.Helpers.DTOs
{
    public class RegisterDTO
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserType UserType { get; set; }

        public List<String> ProductsToSell { get; set; } = new List<string>();

        public string Token { get; set; }
    }
}
