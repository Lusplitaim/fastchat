namespace FastChat.Data.Entities
{
    public class ChatsUsersEntity
    {
        public Guid ChatId { get; set; }
        public int UserId { get; set; }

        public AppUserEntity User { get; set; }
    }
}
