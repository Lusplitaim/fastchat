namespace FastChat.Data.Entities
{
    public class ChatMemberEntity
    {
        public long ChatId { get; set; }
        public int UserId { get; set; }

        public AppUserEntity User { get; set; }
        public ChatEntity Chat { get; set; }
    }
}
