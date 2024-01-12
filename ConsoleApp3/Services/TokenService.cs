using ConsoleApp3.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ConsoleApp3.Services
{
    public class TokenService : ITokenService
    {
        public ClaimsIdentity GenerateClaims()
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim("aud", "https://identity.acesso.io"));
            ci.AddClaim(new Claim("iss", "svcaccene@b874cfcf-3576-494a-8d25-9e03cfd37ecd.iam.acesso.io"));
            ci.AddClaim(new Claim("scope", "*"));
            ci.AddClaim(new Claim("exp", DateTime.UtcNow.ToString()));
            ci.AddClaim(new Claim("iat", DateTime.UtcNow.AddHours(1).ToString()));

            return ci;
        }

        public string GenerateJwtTokenWithRsa256Signature(string privateKey)
        {
            var rsa = RSA.Create();
            rsa.ImportFromPem(privateKey);

            var credentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Subject = GenerateClaims(),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor) as JwtSecurityToken;
            token?.Payload.Remove("nbf");

            return tokenHandler.WriteToken(token);
        }
    }
}
