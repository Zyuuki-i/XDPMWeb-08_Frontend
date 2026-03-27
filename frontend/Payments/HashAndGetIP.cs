using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

namespace frontend.Payments
{
    public class HashAndGetIP
    {
        public static String HmacSHA512(string key, String inputData)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Chuỗi 'key' không được để trống hoặc null.", nameof(key));
            }

            if (string.IsNullOrEmpty(inputData))
            {
                throw new ArgumentException("Chuỗi 'inputData' không được để trống hoặc null.", nameof(inputData));
            }
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }


        internal static string GetIpAddress(HttpContext httpContext)
        {
            string ipAddress = string.Empty;
            try
            {
                ipAddress = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

                if (string.IsNullOrEmpty(ipAddress) || (ipAddress.ToLower() == "unknown") || ipAddress.Length > 45)
                    ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
            }
            catch (Exception ex)
            {
                ipAddress = "Invalid IP:" + ex.Message;
            }

            return ipAddress;
        }
    }
}
