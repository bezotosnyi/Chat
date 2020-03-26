namespace Chat.DAL
{
    using System.Data.Entity;

    using Chat.DAL.Infrastructure;
    using Chat.DAL.Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;

        private IUserRepository userRepository;

        private IMessageRepository messageRepository;

        public UnitOfWork()
        {
            this.dbContext = new ChatDbContext();
        }

        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IUserRepository UserRepository => this.userRepository ?? new UserRepository(this.dbContext);

        public IMessageRepository MessageRepository => this.messageRepository ?? new MessageRepository(this.dbContext);

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }
    }
}