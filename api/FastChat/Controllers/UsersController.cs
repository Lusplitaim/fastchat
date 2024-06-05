using Microsoft.AspNetCore.Mvc;
using FastChat.Core.Services;
using FastChat.Core.Models;
using Microsoft.AspNetCore.Authorization;

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

        [HttpPut("{userId}")]
        public IActionResult EditUser(int userId, EditUser model)
        {
            m_UserService.EditUser(userId, model);
            return Ok();
        }

        [HttpGet("{userId}/chats")]
        public ActionResult<List<Chat>> GetUserChats(int userId)
        {
            return Ok(m_ChatsService.GetChats(userId));
        }

        [HttpGet("{userId}/chat")]
        public IActionResult GetChatWithUser(int userId)
        {
            return Ok(m_ChatsService.GetChatWithUser(userId));
        }
    }
}
