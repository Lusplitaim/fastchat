using FastChat.Core.Repositories;

namespace FastChat.Core.Services
{
    public class ChatsService : IChatsService
    {
        private IUnitOfWork m_UnitOfWork;
        public ChatsService(IUnitOfWork uow)
        {
            m_UnitOfWork = uow;
        }

        public List<string> GetChats(string userName)
        {
            var users = m_UnitOfWork.ChatRepository.Get(userName);

            return users.Select(u => u.UserName).ToList();
        }
    }
}
