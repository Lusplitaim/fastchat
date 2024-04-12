using FastChat.Data.Entities;
using FastChat.Data;
using Microsoft.AspNetCore.Mvc;
using FastChat.Core.Services;

namespace FastChat.Controllers
{
    public class UsersController : BaseController
    {
        private IUserService m_UserService;
        private IChatsService m_ChatsService;
        public UsersController(IUserService userService, IChatsService chatsService)
        {
            m_UserService = userService;
            m_ChatsService = chatsService;
        }

        [HttpGet("{userName}")]
        public IActionResult Get(string userName)
        {
            return Ok(m_UserService.Get(userName));
        }

        [HttpGet("{userId}/chats")]
        public IActionResult GetUserChats(int userId)
        {
            return Ok(m_ChatsService.GetChats(userId));
        }
    }
}
