using JWT2.Services.Abstracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWT2.Services.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration) => _configuration = configuration;
        public string GetToken()
        {
            var claims = new Claim[] { new Claim(ClaimTypes.Name, "emir"), new Claim(ClaimTypes.Role, "Admin") };
            var securtiyKey = new SymmetricSecurityKey(EncodeString(_configuration["jwt:key"]));
            var signingCredentials = new SigningCredentials(securtiyKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: _configuration["jwt:issuer"], audience: _configuration["jwt:audience"], expires: GetDateTime().AddMinutes(5), notBefore: GetDateTime(), signingCredentials: signingCredentials, claims: claims);

            return WriteToken(token);
        }

        public byte[] EncodeString(string key) => System.Text.Encoding.UTF8.GetBytes(key);

        public string WriteToken(JwtSecurityToken token) => new JwtSecurityTokenHandler().WriteToken(token);

        public DateTime GetDateTime() => DateTime.Now;

        public bool ValidateToken(string myToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(
                    token: myToken,
                    validationParameters: new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["jwt:issuer"],
                        ValidateAudience = true,
                        ValidAudience = _configuration["jwt:audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(EncodeString(_configuration["jwt:key"])),
                    }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claims = jwtToken.Claims.ToList();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }



    }
}
