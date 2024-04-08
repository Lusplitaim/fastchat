namespace FastChat.Core.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IChatRepository ChatRepository { get; }
    }
}
