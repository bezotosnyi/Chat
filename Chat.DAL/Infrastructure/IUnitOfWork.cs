namespace Chat.DAL.Infrastructure
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IMessageRepository MessageRepository { get; }

        void Commit();
    }
}