using FastChat.Core.Models;
using FastChat.Core.Repositories;
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
        private readonly IUnitOfWork m_UnitOfWork;

        public AuthController(IAuthService authService, IUnitOfWork uow)
        {
            m_AuthService = authService;
            m_UnitOfWork = uow;
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

            var appUser = m_UnitOfWork.UserRepository.GetByEmail(model.Email);
            if (appUser is null)
            {
                throw new Exception("User not found");
            }

            return Ok(new { Token = CreateAuthToken(model.Email, appUser.Id), User = AppUser.FromEntity(appUser) });
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignUser model)
        {
            await m_AuthService.SignInAsync(model);

            var appUser = m_UnitOfWork.UserRepository.GetByEmail(model.Email);
            if (appUser is null)
            {
                throw new Exception("User not found");
            }

            return Ok(new { Token = CreateAuthToken(model.Email, appUser.Id), User = AppUser.FromEntity(appUser) });
        }

        private string CreateAuthToken(string email, int id)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Email, email), new Claim(ClaimTypes.NameIdentifier, id.ToString()) };
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
