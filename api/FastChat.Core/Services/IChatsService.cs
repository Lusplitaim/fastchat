namespace FastChat.Core.Services
{
    public interface IChatsService
    {
        List<string> GetChats(string userName);
    }
}