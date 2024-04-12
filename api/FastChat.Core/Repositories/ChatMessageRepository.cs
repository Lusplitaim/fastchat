using FastChat.Data;
using FastChat.Data.Entities;

namespace FastChat.Core.Repositories
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private DatabaseContext m_DatabaseContext;
        public ChatMessageRepository(DatabaseContext context)
        {
            m_DatabaseContext = context;
        }

        public async Task<ChatMessageEntity> SaveAsync(long chatId, int authorId, string messageContent)
        {
            ChatMessageEntity message = new()
            {
                ChatId = chatId,
                Content = messageContent,
                AuthorId = authorId,
            };
            return (await m_DatabaseContext.ChatMessages.AddAsync(message)).Entity;
        }
    }
}
