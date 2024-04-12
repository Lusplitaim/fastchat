namespace FastChat.Core.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IChatRepository ChatRepository { get; }
        IChatMessageRepository ChatMessageRepository { get; }
        IChannelRepository ChannelRepository { get; }
        void SaveChanges();
    }
}
