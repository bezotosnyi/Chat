namespace Chat.DAL.Infrastructure
{
    using Chat.Domain.Entities;

    public interface IMessageRepository : IRepository<Message>
    {
    }
}