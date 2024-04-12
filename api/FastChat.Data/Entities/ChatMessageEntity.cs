namespace FastChat.Data.Entities
{
    public class ChatMessageEntity
    {
        public long ChatId { get; set; }
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? AuthorId { get; set; }
        public AppUserEntity? Author { get; set; }
        public ChatEntity Chat { get; set; }
    }
}
