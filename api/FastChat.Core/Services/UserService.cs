using FastChat.Core.Repositories;
using FastChat.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FastChat.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor m_HttpContextAccessor;
        private readonly IUnitOfWork m_UnitOfWork;

        public UserService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            m_HttpContextAccessor = httpContextAccessor;
            m_UnitOfWork = unitOfWork;
        }

        public AppUserEntity Get(string userName)
        {
            var user = m_UnitOfWork.UserRepository.Get(userName);
            if (user is null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public int? GetAuthUserId()
        {
            string? id = m_HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return id is null ? default : Convert.ToInt32(id);
        }

        public string? GetAuthUserName()
        {
            return m_HttpContextAccessor.HttpContext.User.Identity?.Name;
        }
    }
}
