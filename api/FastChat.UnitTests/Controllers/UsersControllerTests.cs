using FastChat.Controllers;
using FastChat.Core.Models;
using FastChat.Core.Services;
using FastChat.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FastChat.UnitTests.Controllers
{
    public class UsersControllerTests
    {
        [Fact]
        public void GetUserChats_ReturnsAllUserChats()
        {
            var userId = 1;
            var userServiceMock = new Mock<IUserService>();
            var chatsServiceMock = new Mock<IChatsService>();
            chatsServiceMock.Setup(m => m.GetChats(userId)).Returns(GetUserChats);
            var controller = new UsersController(userServiceMock.Object, chatsServiceMock.Object);

            var result = controller.GetUserChats(userId);

            var actionResult = Assert.IsType<ActionResult<List<Chat>>>(result);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var value = Assert.IsType<List<Chat>>(objectResult.Value);
            Assert.Equal(2, value.Count());
        }

        [Fact]
        public void EditUser_ReturnsOk()
        {
            var userId = 1;
            EditUser model = new();
            var userServiceMock = new Mock<IUserService>();
            var chatsServiceMock = new Mock<IChatsService>();
            var controller = new UsersController(userServiceMock.Object, chatsServiceMock.Object);

            var result = controller.EditUser(userId, model);

            var objectResult = Assert.IsType<OkResult>(result);
        }

        private List<Chat> GetUserChats()
        {
            return new()
            {
                new Chat { Id = 1, Name = "Name", Type = ChatType.Dialog },
                new Chat { Id = 2, Name = "SomeName", Type = ChatType.Dialog },
            };
        }
    }
}
