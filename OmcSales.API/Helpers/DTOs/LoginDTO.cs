using System;
namespace OmcSales.API.Helpers.DTOs
{
    public class LoginDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
