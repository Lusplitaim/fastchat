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

        [HttpGet("search/{userName}")]
        public IActionResult FindChats(string userName)
        {
            var chats = m_ChatsService.FindChats(userName);
            return Ok(chats);
        }

        [HttpGet("{chatId}")]
        public IActionResult FindChats(long chatId)
        {
            var chat = m_ChatsService.GetChat(chatId);
            return Ok(chat);
        }
    }
}
