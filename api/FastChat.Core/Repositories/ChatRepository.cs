using FastChat.Data;
using FastChat.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastChat.Core.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private DatabaseContext m_DatabaseContext;
        public ChatRepository(DatabaseContext context)
        {
            m_DatabaseContext = context;
        }

        public List<ChatEntity> Get(int userId)
        {
            List<ChatEntity> chats = m_DatabaseContext.Chats
                .Include(c => c.Members)
                .Where(c => c.Members.Contains(new() { Id = userId }))
                .AsNoTracking()
                .ToList();

            return chats;
        }

        public ChatEntity? Get(string linkName)
        {
            return m_DatabaseContext.Chats
                .AsNoTracking()
                .SingleOrDefault(e => e.LinkName == linkName);
        }

        public ChatEntity? GetDialog(int senderId, int recipientId)
        {
            List<int> userIds = [senderId, recipientId];
            return m_DatabaseContext.ChatMembers
                .Include(cm => cm.Chat)
                .Where(cm => userIds.Contains(cm.UserId)).Select(cm => cm.Chat)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public async Task<ChatEntity> CreateDialogAsync(int senderId, int recipientId)
        {
            AppUserEntity sender = new() { Id = senderId };
            AppUserEntity recipient = new() { Id = recipientId };
            ChatEntity chat = new()
            {
                Members = [sender, recipient],
            };
            return (await m_DatabaseContext.Chats.AddAsync(chat)).Entity;
        }
    }
}