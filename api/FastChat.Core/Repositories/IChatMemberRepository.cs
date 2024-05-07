using FastChat.Data.Entities;

namespace FastChat.Core.Repositories
{
    public interface IChatMemberRepository
    {
        ChatMemberEntity? GetDialogRecipient(long chatId, int currentUserId);
        List<ChatMemberEntity> GetDialogRecipients(int userId);
    }
}