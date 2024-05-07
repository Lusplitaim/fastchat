using FastChat.Data.Models;

namespace FastChat.Core.Models
{
    public class Chat
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public ChatType Type { get; set; }
        public ChatUser? Recipient { get; set; }
        public ChatChannel? Channel { get; set; }
        public List<ChatMessage> Messages { get; set; } = [];
    }
}
