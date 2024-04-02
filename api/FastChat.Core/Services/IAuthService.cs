namespace FastChat.Core.Services
{
    public interface IAuthService
    {
        void SignInAsync();
        void SignOutAsync();
    }
}
