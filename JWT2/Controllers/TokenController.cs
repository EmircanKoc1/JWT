using JWT2.Models;
using JWT2.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT2.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        ITokenService _tokenService;

        public TokenController(ITokenService tokenService) => _tokenService = tokenService;

        [HttpGet("[action]")]
        public ActionResult<TokenModel> GetToken() => new TokenModel
        {
            Token = _tokenService.GetToken(),
            ExpireDate = DateTime.Now.AddMinutes(5),
        };

        [HttpGet("[action]"),Authorize(Roles ="User")]
        public IActionResult TryToken() => Ok("User Yetkilendirme Başarılı");

        
        [HttpGet("[action]"),Authorize(Roles ="Admin")]
        public IActionResult TryTokenAdmin() => Ok("Admin Yetkilendirmesi Başarılı");

        [HttpPost("[action]")]
        public IActionResult TokenValidate([FromForm]string token) => _tokenService.ValidateToken(token) ? Ok("Yetkilendirme Başarılı") : Unauthorized("Yetkilendirme Başarısız");
    }
}

