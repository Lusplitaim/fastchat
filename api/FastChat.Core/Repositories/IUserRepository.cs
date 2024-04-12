using FastChat.Data.Entities;

namespace FastChat.Core.Repositories
{
    public interface IUserRepository
    {
        AppUserEntity? Get(string userName);
        AppUserEntity? Get(int id);
        List<AppUserEntity> Find(string keyword);
    }
}
