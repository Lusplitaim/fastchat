using FastChat.Data;
using FastChat.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastChat.Core.Repositories
{
    public class ChannelRepository : IChannelRepository
    {
        private DatabaseContext m_DatabaseContext;
        public ChannelRepository(DatabaseContext context)
        {
            m_DatabaseContext = context;
        }

        public List<ChannelEntity> Find(string keyword)
        {
            return m_DatabaseContext.Channels
                .AsNoTracking()
                .Where(c => c.SearchName.ToLower().Contains(keyword) || c.Name.ToLower().Contains(keyword))
                .Include(c => c.Chat)
                .ToList();
        }

        public List<ChannelEntity> Get(List<long> chatIds)
        {
            return m_DatabaseContext.Channels
                .AsNoTracking()
                .Where(c => chatIds.Contains(c.ChatId))
                .Include(c => c.Chat)
                .ToList();
        }
    }
}