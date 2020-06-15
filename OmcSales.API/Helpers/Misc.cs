using System;
namespace OmcSales.API.Helpers
{
    public static class Misc
    {
       public static string GenerateToken(string salt)
        {
            return salt + DateTime.Now.Second;
        }
    }
}
