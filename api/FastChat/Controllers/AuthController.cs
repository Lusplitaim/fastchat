
using Microsoft.AspNetCore.Mvc;

namespace FastChat.Controllers
{
    public class AuthController : BaseController
    {
        [HttpGet("sign-in")]
        public IActionResult SignIn()
        {
            return Ok();
        }
    }
}
