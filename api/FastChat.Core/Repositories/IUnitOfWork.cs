namespace FastChat.Core.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IChatRepository ChatRepository { get; }
        IChatMessageRepository ChatMessageRepository { get; }
        IChatMemberRepository ChatMemberRepository { get; }
        IChannelRepository ChannelRepository { get; }
        void SaveChanges();
    }
}
