using FastChat.Core.Models;
using FastChat.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FastChat.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IAuthService m_AuthService;

        public AuthController(IAuthService authService)
        {
            m_AuthService = authService;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            return Ok("Hello there");
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(CreateUser model)
        {
            await m_AuthService.SignUpAsync(model);

            return Ok(CreateAuthToken(model.Email));
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignUser model)
        {
            await m_AuthService.SignInAsync(model);

            return Ok(CreateAuthToken(model.Email));
        }

        private string CreateAuthToken(string email)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Email, email) };
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(5)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
