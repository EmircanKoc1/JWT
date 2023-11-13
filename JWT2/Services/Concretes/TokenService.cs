using JWT2.Services.Abstracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWT2.Services.Concrete
{
    public class TokenService : ITokenService
    {
        IConfiguration configuration;

        public TokenService(IConfiguration configuration) => this.configuration = configuration;
        public string GetToken()
        {
            var claims = new Claim[] { new Claim(ClaimTypes.Name,"emir"),new Claim(ClaimTypes.Role,"Admin") };
            var securtiyKey = new SymmetricSecurityKey(EncodeString(configuration["jwt:key"]));
            var signingCredentials = new SigningCredentials(securtiyKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: configuration["jwt:issuer"], audience: configuration["jwt:audience"], expires: GetDateTime().AddMinutes(5), notBefore: GetDateTime(), signingCredentials: signingCredentials,claims:claims);

            return WriteToken(token);
        }

        public byte[] EncodeString(string key) => System.Text.Encoding.UTF8.GetBytes(key);

        public string WriteToken(JwtSecurityToken token) => new JwtSecurityTokenHandler().WriteToken(token);

        public DateTime GetDateTime() => DateTime.Now;

    }
}
