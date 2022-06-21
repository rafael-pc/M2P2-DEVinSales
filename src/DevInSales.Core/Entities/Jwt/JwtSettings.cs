using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Entities.Jwt
{
    public static class JwtSettings
    {
        public static string Secrets { get; set; }
        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static byte[] Key { get; set; }
    }
}
