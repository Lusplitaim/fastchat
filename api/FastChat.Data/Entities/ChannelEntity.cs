namespace FastChat.Data.Entities
{
    public class ChannelEntity
    {
        public long Id { get; set; }
        public string SearchName { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public long ChatId { get; set; }
        public int OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Settings { get; set; }
        public AppUserEntity Owner { get; set; }
        public ChatEntity Chat { get; set; }
    }
}
