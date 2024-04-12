using FastChat.Core.Models;
using FastChat.Core.Repositories;
using FastChat.Data.Entities;
using FastChat.Data.Models;

namespace FastChat.Core.Services
{
    public class ChatsService : IChatsService
    {
        private IUnitOfWork m_UnitOfWork;
        public ChatsService(IUnitOfWork uow)
        {
            m_UnitOfWork = uow;
        }

        public List<Chat> GetChats(int userId)
        {
            var user = m_UnitOfWork.UserRepository.Get(userId);
            if (user is null)
            {
                throw new Exception("User not found");
            }

            var chats = m_UnitOfWork.ChatRepository.Get(user.Id);
            var channels = m_UnitOfWork.ChannelRepository.Get(chats.Select(c => c.Id).ToList());
            m_UnitOfWork.UserRepository.Get();

            List<Chat> result = [];
            foreach (var chat in chats)
            {
                switch (chat.Type)
                {
                    case ChatType.Dialog:
                        result.Add(new() { Id = chat.Id, Name =  });
                        break;
                }
            }

            return chats;
        }

        public List<SearchChat> FindChats(string keyword)
        {
            var users = m_UnitOfWork.UserRepository.Find(keyword);
            var channels = m_UnitOfWork.ChannelRepository.Find(keyword);

            List<SearchChat> result = [];
            foreach (var user in users)
            {
                result.Add(new() { Id = null, Name = string.Join(' ', user.FirstName, user.LastName), Type = ChatType.Dialog });
            }

            foreach (var channel in channels)
            {
                result.Add(new() { Id = null, Name = channel.Name, Type = channel.Chat.Type });
            }

            return result;
        }

        public ChatEntity GetChat(string searchName)
        {
            var chat = m_UnitOfWork.ChatRepository.Get(searchName);
            if (chat is null)
            {
                throw new Exception("Chat is not found");
            }

            return chat;
        }
    }
}
