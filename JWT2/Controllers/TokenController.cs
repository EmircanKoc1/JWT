using JWT2.Models;
using JWT2.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT2.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        ITokenService _tokenService;

        public TokenController(ITokenService tokenService) => _tokenService = tokenService;

        [HttpGet("[action]"),AllowAnonymous]
        public ActionResult<TokenModel> GetToken() => new TokenModel
        {
            Token = _tokenService.GetToken(),
            ExpireDate = DateTime.Now.AddMinutes(5),
        };

        [HttpGet("[action]")]
        public IActionResult TryToken() => Ok("Yetkilendirme Başarılı");



    }
}

