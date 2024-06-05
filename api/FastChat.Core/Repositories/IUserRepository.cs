using FastChat.Core.Models;
using FastChat.Data.Entities;

namespace FastChat.Core.Repositories
{
    public interface IUserRepository
    {
        AppUserEntity? Get(string userName);
        AppUserEntity? GetByEmail(string email);
        AppUserEntity? Get(int id);
        AppUserEntity Update(EditUser user);
        List<AppUserEntity> Get(List<int> ids);
        List<AppUserEntity> Find(string keyword);
    }
}
