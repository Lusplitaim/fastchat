using FastChat.Data.Entities;

namespace FastChat.Core.Repositories
{
    public interface IChannelRepository
    {
        List<ChannelEntity> Find(string keyword);
        List<ChannelEntity> Get(List<long> chatIds);
    }
}