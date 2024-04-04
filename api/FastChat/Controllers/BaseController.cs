using Microsoft.AspNetCore.Mvc;

namespace FastChat.Controllers
{
    [Route("[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
    }
}
