using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastChat.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
    }
}
