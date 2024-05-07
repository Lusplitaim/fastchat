using FastChat.Data;
using FastChat.Data.Entities;
using FastChat.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FastChat.Core.Repositories
{
    public class ChatMemberRepository : IChatMemberRepository
    {
        private DatabaseContext m_DatabaseContext;
        public ChatMemberRepository(DatabaseContext context)
        {
            m_DatabaseContext = context;
        }

        public ChatMemberEntity? GetDialogRecipient(long chatId, int currentUserId)
        {
            return m_DatabaseContext.ChatMembers
                .AsNoTracking()
                .Include(cm => cm.User)
                .Include(cm => cm.Chat)
                .SingleOrDefault(c => c.Chat.Type == ChatType.Dialog && c.ChatId == chatId && c.User.Id != currentUserId);
        }

        public List<ChatMemberEntity> GetDialogRecipients(int userId)
        {
            var chatIds = m_DatabaseContext.ChatMembers
                .AsNoTracking()
                .Where(cm => cm.UserId == userId)
                .Select(cm => cm.ChatId);

            return [.. m_DatabaseContext.ChatMembers
                .AsNoTracking()
                .Include(cm => cm.Chat)
                .Include(cm => cm.User)
                .Where(cm => chatIds.Contains(cm.ChatId) &&
                    (cm.Chat.Type == ChatType.Dialog && cm.UserId != userId ||
                    cm.Chat.Type == ChatType.Personal && cm.UserId == userId))];

        }
    }
}
