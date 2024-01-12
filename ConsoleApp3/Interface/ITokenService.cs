using System.Security.Claims;

namespace ConsoleApp3.Interface
{
    public interface ITokenService
    {
        string GenerateJwtTokenWithRsa256Signature(string privateKey);
        ClaimsIdentity GenerateClaims();


    }
}
