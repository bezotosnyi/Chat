namespace Chat.Service.Front.Console.Configuration
{
    using System.Data.Entity;

    using Autofac;

    using Chat.BLL.Infrastructure;
    using Chat.BLL.Services;
    using Chat.DAL;
    using Chat.DAL.Infrastructure;
    using Chat.DAL.Repositories;
    using Chat.Domain.Entities;

    public static class AutofacConfig
    {
        public static IContainer Configure()
        {
            var containerBuilder = new ContainerBuilder();

            // DAL dependencies
            containerBuilder.RegisterType<ChatDbContext>().As<DbContext>();
            containerBuilder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IRepository<>));
            containerBuilder.RegisterType<UserRepository>().As<IRepository<User>>();
            containerBuilder.RegisterType<UserRepository>().As<IUserRepository>();
            containerBuilder.RegisterType<MessageRepository>().As<IRepository<Message>>();
            containerBuilder.RegisterType<MessageRepository>().As<IMessageRepository>();
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            // BLL dependencies
            containerBuilder.RegisterType<LoggingService>().As<ILoggingService>();
            containerBuilder.RegisterType<OperationStatusService>().As<IOperationStatusService>();
            containerBuilder.RegisterGeneric(typeof(BaseService<,>)).As(typeof(IBaseService<,>));
            containerBuilder.RegisterType<UserService>().As<IUserService>();
            containerBuilder.RegisterType<MessageService>().As<IMessageService>();

            // WCF
            containerBuilder.RegisterType<ChatService>().As<IChatService>();

            return containerBuilder.Build();
        }
    }
}