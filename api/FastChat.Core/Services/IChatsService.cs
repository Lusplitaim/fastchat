using FastChat.Core.Models;

namespace FastChat.Core.Services
{
    public interface IChatsService
    {
        List<Chat> GetChats(int userId);
        List<Chat> FindChats(string keyword);
        Chat GetChat(long chatId);
        Chat GetChatWithUser(int userId);
    }
}