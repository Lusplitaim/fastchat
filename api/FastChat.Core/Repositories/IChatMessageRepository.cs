using FastChat.Data.Entities;

namespace FastChat.Core.Repositories
{
    public interface IChatMessageRepository
    {
        Task<ChatMessageEntity> SaveAsync(long chatId, int authorId, string messageContent);
    }
}