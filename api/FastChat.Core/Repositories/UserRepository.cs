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
                .Where(u => string.Join(' ', u.FirstName, u.LastName).ToLower().Contains(keyword))
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
    }
}
