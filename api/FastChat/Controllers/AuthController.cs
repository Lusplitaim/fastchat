
using FastChat.Core.Models;
using FastChat.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FastChat.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService m_AuthService;

        public AuthController(IAuthService authService)
        {
            m_AuthService = authService;
        }

        [HttpGet("sign-up")]
        public async Task<IActionResult> SignUp(CreateUser model)
        {
            await m_AuthService.SignUpAsync(model);

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, model.UserName) };
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(5)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
        }
    }
}
