using System.IdentityModel.Tokens.Jwt;

namespace JWT2.Services.Abstracts
{
    public interface ITokenService
    {
        string GetToken();

        byte[] EncodeString(string key);

        string WriteToken(JwtSecurityToken token);

        bool ValidateToken(string myToken);


    }
}
