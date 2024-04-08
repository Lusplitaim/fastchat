using FastChat.Data;

namespace FastChat.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext m_DatabaseContext;
        public UserRepository(DatabaseContext context)
        {
            m_DatabaseContext = context;
        }
    }
}
