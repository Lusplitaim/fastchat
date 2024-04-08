using FastChat.Data;
using FastChat.Data.Entities;

namespace FastChat.Core.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private DatabaseContext m_DatabaseContext;
        public ChatRepository(DatabaseContext context)
        {
            m_DatabaseContext = context;
        }

        public List<AppUserEntity> Get(string userName)
        {
            return m_DatabaseContext.Users.Where(e => e.UserName.StartsWith(userName)).ToList();
        }
    }
}
