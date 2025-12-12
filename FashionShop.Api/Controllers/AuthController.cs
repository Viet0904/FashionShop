using FashionShop.Application.Auth;
using FashionShop.Contracts.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest req, CancellationToken ct)
        {
            var res = await _auth.RegisterAsync(req, ct);
            if (!res.Succeeded) return BadRequest(res);
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest req, CancellationToken ct)
        {
            var res = await _auth.LoginAsync(req, ct);
            if (!res.Succeeded) return BadRequest(res);
            return Ok(res);
        }
    }
}
