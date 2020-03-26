namespace Chat.DAL
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Chat.Domain.Entities;

    public class ChatDbContext : DbContext
    {
        public ChatDbContext()
            : base("ChatDbContext")
        {
        }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<Message> Message { get; set; }
    }
}