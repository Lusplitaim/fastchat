using FastChat.Core.Models;
using FastChat.Data.Entities;

namespace FastChat.Core.Services
{
    public interface IUserService
    {
        AppUserEntity Get(string userName);
        void EditUser(int userId, EditUser model);
    }
}