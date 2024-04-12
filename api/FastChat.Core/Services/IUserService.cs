using FastChat.Data.Entities;

namespace FastChat.Core.Services
{
    public interface IUserService
    {
        int? GetAuthUserId();
        string? GetAuthUserName();
        AppUserEntity Get(string userName);
    }
}