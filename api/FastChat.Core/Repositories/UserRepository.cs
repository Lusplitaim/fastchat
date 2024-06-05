using FastChat.Core.Models;
using FastChat.Data;
using FastChat.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastChat.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext m_DatabaseContext;
        public UserRepository(DatabaseContext context)
        {
            m_DatabaseContext = context;
        }

        public List<AppUserEntity> Find(string keyword)
        {
            return m_DatabaseContext.Users
                .AsNoTracking()
                .Where(u => u.FirstName.ToLower().Contains(keyword) || u.LastName != null && u.LastName.ToLower().Contains(keyword))
                .ToList();
        }

        public AppUserEntity? Get(string userName)
        {
            return m_DatabaseContext.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public AppUserEntity? Get(int id)
        {
            return m_DatabaseContext.Users.SingleOrDefault(u => u.Id == id);
        }

        public List<AppUserEntity> Get(List<int> ids)
        {
            return m_DatabaseContext.Users
                .AsNoTracking()
                .Where(u => ids.Contains(u.Id))
                .ToList();
        }

        public AppUserEntity? GetByEmail(string email)
        {
            return m_DatabaseContext.Users.SingleOrDefault(u => u.Email == email);
        }

        public AppUserEntity Update(EditUser user)
        {
            throw new NotImplementedException();
        }
    }
}
