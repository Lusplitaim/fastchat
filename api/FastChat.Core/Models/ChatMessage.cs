using FastChat.Data.Entities;

namespace FastChat.Core.Models
{
    public class ChatMessage
    {
        public long ChatId { get; set; }
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? AuthorId { get; set; }

        public static ChatMessage CreateFrom(ChatMessageEntity entity)
        {
            return new()
            {
                ChatId = entity.ChatId,
                Id = entity.Id,
                Content = entity.Content,
                CreatedAt = entity.CreatedAt,
                AuthorId = entity.AuthorId,
            };
        }
    }
}
