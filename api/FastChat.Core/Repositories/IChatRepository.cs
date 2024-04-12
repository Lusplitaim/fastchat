using FastChat.Data.Entities;

namespace FastChat.Core.Repositories
{
    public interface IChatRepository
    {
        List<ChatEntity> Get(int userId);
        ChatEntity? Get(string linkName);
        ChatEntity? GetDialog(int senderId, int recipientId);
        Task<ChatEntity> CreateDialogAsync(int senderId, int recipientId);
    }
}