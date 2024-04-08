using FastChat.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastChat.Controllers
{
    public class ChatsController : BaseController
    {
        private IChatsService m_ChatsService { get; }

        public ChatsController(IChatsService chatsService)
        {
            m_ChatsService = chatsService;
        }

        [HttpGet("{userName}")]
        public IActionResult GetChats(string userName)
        {
            var userNames = m_ChatsService.GetChats(userName);
            return Ok(userNames);
        }
    }
}
