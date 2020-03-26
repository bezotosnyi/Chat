namespace Chat.DAL.Repositories
{
    using System.Data.Entity;

    using Chat.DAL.Infrastructure;
    using Chat.Domain.Entities;

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }
    }
}