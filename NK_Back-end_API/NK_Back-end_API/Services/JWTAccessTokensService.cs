using NK_Back_end_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NK_Back_end_API.Models;
using Jose;
using System.Text;

namespace NK_Back_end_API.Services
{
    public class JWTAccessTokensService : IAccessTokensService
    {
        private byte[] secretKey = Encoding.UTF8.GetBytes("C# ASP NK API");

        // ใช้ https://jwt.io/  เพื่อสร้าง AccessTokens
        public string GenerateAccessTokens(string email, int minte = 60)
        {
            try
            {
                JWTPayload payload = new JWTPayload {
                    email=email,
                    exp= DateTime.UtcNow.AddMinutes(minte)
                };

                return JWT.Encode(payload,this.secretKey, JwsAlgorithm.HS256);
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }


        public class JWTPayload
        {
            public String email { get; set; }
            public DateTime exp { get; set; }
        }
    }
}