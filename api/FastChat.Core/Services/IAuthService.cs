using FastChat.Core.Models;

namespace FastChat.Core.Services
{
    public interface IAuthService
    {
        Task SignUpAsync(CreateUser model);
        Task SignInAsync();
        Task SignOutAsync();
    }
}
