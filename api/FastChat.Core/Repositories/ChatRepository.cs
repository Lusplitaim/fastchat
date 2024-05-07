using FastChat.Data;
using FastChat.Data.Entities;
using FastChat.Data.Models;
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

        public List<ChatEntity> GetUserChats(int userId)
        {
            List<ChatEntity> chats = m_DatabaseContext.Chats
                .Include(c => c.Members)
                .Include(c => c.Messages)
                .Where(c => c.Members.Contains(new() { Id = userId }))
                .AsNoTracking()
                .ToList();

            return chats;
        }

        public ChatEntity? GetDialog(int senderId, int recipientId)
        {
            if (senderId == recipientId)
            {
                return m_DatabaseContext.ChatMembers
                    .AsNoTracking()
                    .Include(cm => cm.Chat)
                    .Where(cm => cm.UserId == senderId && cm.Chat.Type == ChatType.Personal)
                    .Select(cm => cm.Chat)
                    .FirstOrDefault();
            }

            List<int> userIds = [senderId, recipientId];
            var chatId = m_DatabaseContext.ChatMembers
                .AsNoTracking()
                .Include(cm => cm.Chat)
                .Where(cm => userIds.Contains(cm.UserId) && cm.Chat.Type == ChatType.Dialog)
                .GroupBy(cm => cm.ChatId)
                .Where(g => g.Count() == 2)
                .Select(g => g.Key)
                .FirstOrDefault();
            return m_DatabaseContext.Chats
                .SingleOrDefault(c => c.Id == chatId);
        }

        public async Task<ChatEntity> CreateDialogAsync(int senderId, int recipientId)
        {
            var sender = m_DatabaseContext.Users.Single(u => u.Id == senderId);
            var recipient = m_DatabaseContext.Users.Single(u => u.Id == recipientId);
            ChatEntity chat = new();
            if (senderId == recipientId)
            {
                chat.Type = ChatType.Personal;
                chat.Members = [sender];
            }
            else
            {
                chat.Type = ChatType.Dialog;
                chat.Members = [sender, recipient];
            }
            return (await m_DatabaseContext.Chats.AddAsync(chat)).Entity;
        }

        public ChatEntity? Get(long chatId)
        {
            return m_DatabaseContext.Chats
                .AsNoTracking()
                .SingleOrDefault(c => c.Id == chatId);
        }
    }
}