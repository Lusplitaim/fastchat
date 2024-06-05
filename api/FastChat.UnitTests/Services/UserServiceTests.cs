using FastChat.Core.Exceptions;
using FastChat.Core.Models;
using FastChat.Core.Repositories;
using FastChat.Core.Services;
using FastChat.Core.Utils;
using FastChat.Data.Entities;
using Moq;

namespace FastChat.UnitTests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void EditUser_WhenEditOtherUser_ThrowsForbidden()
        {
            int userId = 0;
            EditUser model = new();
            var authUtilsMock = new Mock<IAuthUtils>();
            authUtilsMock.Setup(m => m.GetAuthUserId()).Returns(1);
            UserService service = new(new Mock<IUnitOfWork>().Object, authUtilsMock.Object);

            Assert.Throws<ForbiddenCoreException>(() => service.EditUser(userId, model));
        }

        [Fact]
        public void EditUser_WhenUserNotFound_ThrowsNotFound()
        {
            int userId = 0;
            EditUser model = new();
            var authUtilsMock = new Mock<IAuthUtils>();
            authUtilsMock.Setup(m => m.GetAuthUserId()).Returns(userId);
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(m => m.UserRepository.Get(userId)).Returns(() => null);
            UserService service = new(uowMock.Object, authUtilsMock.Object);

            Assert.Throws<NotFoundCoreException>(() => service.EditUser(userId, model));
        }

        [Fact]
        public void EditUser_UserIsUpdated()
        {
            int userId = 0;
            EditUser model = new();
            var authUtilsMock = new Mock<IAuthUtils>();
            authUtilsMock.Setup(m => m.GetAuthUserId()).Returns(userId);
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(m => m.UserRepository.Get(userId)).Returns(() => new AppUserEntity());
            uowMock.Setup(m => m.UserRepository.Update(model)).Returns(() => new AppUserEntity());
            UserService service = new(uowMock.Object, authUtilsMock.Object);

            service.EditUser(userId, model);

            uowMock.Verify((m) => m.UserRepository.Update(model), Times.Once());
        }
    }
}
