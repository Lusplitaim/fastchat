using FastChat.Data;

namespace FastChat.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository => new UserRepository(m_DatabaseContext);
        public IChatRepository ChatRepository => new ChatRepository(m_DatabaseContext);

        private DatabaseContext m_DatabaseContext;
        public UnitOfWork(DatabaseContext context)
        {
            m_DatabaseContext = context;
        }
    }
}
