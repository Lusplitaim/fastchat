using FastChat.Data.Entities;

namespace FastChat.Core.Repositories
{
    public interface IChatRepository
    {
        List<ChatEntity> GetUserChats(int userId);
        ChatEntity? Get(long chatId);
        ChatEntity? GetDialog(int senderId, int recipientId);
        Task<ChatEntity> CreateDialogAsync(int senderId, int recipientId);
    }
}