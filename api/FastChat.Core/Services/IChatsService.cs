using FastChat.Core.Models;
using FastChat.Data.Entities;

namespace FastChat.Core.Services
{
    public interface IChatsService
    {
        List<Chat> GetChats(int userId);
        List<SearchChat> FindChats(string keyword);
        ChatEntity GetChat(string linkName);
    }
}