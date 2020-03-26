namespace Chat.DAL.Repositories
{
    using System.Data.Entity;

    using Chat.DAL.Infrastructure;
    using Chat.Domain.Entities;

    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(DbContext context) : base(context)
        {
        }
    }
}