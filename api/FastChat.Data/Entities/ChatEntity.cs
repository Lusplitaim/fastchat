using FastChat.Data.Models;

namespace FastChat.Data.Entities
{
    public class ChatEntity
    {
        public long Id { get; set; }
        public ChatType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<AppUserEntity> Members { get; set;} = [];
        public ICollection<ChatMessageEntity> Messages { get; set;} = [];
    }
}
