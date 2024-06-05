using FastChat.Core.Exceptions;
using FastChat.Core.Models;
using FastChat.Core.Repositories;
using FastChat.Core.Utils;
using FastChat.Data.Entities;

namespace FastChat.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork m_UnitOfWork;
        private readonly IAuthUtils m_AuthUtils;

        public UserService(IUnitOfWork unitOfWork, IAuthUtils authUtils)
        {
            m_UnitOfWork = unitOfWork;
            m_AuthUtils = authUtils;
        }

        public void EditUser(int userId, EditUser model)
        {
            if (m_AuthUtils.GetAuthUserId() != userId)
            {
                throw new ForbiddenCoreException("Cannot edit another user");
            }

            try
            {
                var user = m_UnitOfWork.UserRepository.Get(userId);
                if (user is not null)
                {
                    m_UnitOfWork.UserRepository.Update(model);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to edit user", ex);
            }

            throw new NotFoundCoreException("User not found");
        }

        public AppUserEntity Get(string userName)
        {
            try
            {
                var user = m_UnitOfWork.UserRepository.Get(userName);
                if (user is not null)
                {
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to edit user", ex);
            }

            throw new NotFoundCoreException("User not found");
        }
    }
}
