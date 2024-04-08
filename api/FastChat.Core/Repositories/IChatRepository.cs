using FastChat.Data.Entities;

namespace FastChat.Core.Repositories
{
    public interface IChatRepository
    {
        List<AppUserEntity> Get(string userName);
    }
}