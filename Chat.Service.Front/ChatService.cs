namespace Chat.Service.Front
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    using Chat.BLL.Infrastructure;
    using Chat.DTO;
    using Chat.DTO.Response;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ChatService : IChatService
    {
        private readonly IUserService userService;

        private readonly IMessageService messageService;

        private readonly ILoggingService loggingService;

        public ChatService(IUserService userService, IMessageService messageService, ILoggingService loggingService)
        {
            this.userService = userService;
            this.messageService = messageService;
            this.loggingService = loggingService;
        }

        public OperationStatusInfo<string> Registration(UserDTO userDto)
        {
            this.loggingService.Info($"Пользователь {this.GetClientAddress()} вызвал {nameof(this.Registration)} метод");
            return this.userService.Registration(userDto);
        }

        public OperationStatusInfo<UserDTO> Login(UserLoginDTO userLoginDto)
        {
            this.loggingService.Info($"Пользователь {this.GetClientAddress()} вызвал {nameof(this.Login)} метод");
            return this.userService.Login(userLoginDto);
        }

        public OperationStatusInfo<List<MessageDTO>> GetAllMessages(UserDTO userDto)
        {
            this.loggingService.Info($"Пользователь {this.GetClientAddress()} вызвал {nameof(this.GetAllMessages)} метод");
            return this.messageService.GetAllMessages(userDto);
        }

        public OperationStatusInfo<List<UserDTO>> GetAllUsers()
        {
            this.loggingService.Info($"Пользователь {this.GetClientAddress()} вызвал {nameof(this.GetAllUsers)} метод");
            return this.userService.GetAllUsers();
        }

        public OperationStatusInfo<string> SendMessage(MessageDTO messageDto)
        {
            this.loggingService.Info($"Пользователь {this.GetClientAddress()} вызвал {nameof(this.SendMessage)} метод");
            return this.messageService.SendMessage(messageDto);
        }

        private string GetClientAddress()
        {
            var context = OperationContext.Current;
            var prop = context.IncomingMessageProperties;
            var endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            return endpoint?.Address;
        }
    }
}