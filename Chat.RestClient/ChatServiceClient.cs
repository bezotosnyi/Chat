namespace Chat.RestClient
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    using Chat.DTO;
    using Chat.DTO.Response;
    using Chat.RestClient.Infrastructure;

    public class ChatServiceClient : ClientBase<IChatService>, IChatService
    {
        public ChatServiceClient(string address)
            : base(new WebHttpBinding(), new EndpointAddress(address))
        {
            this.Endpoint.Behaviors.Add(new WebHttpBehavior());
        }

        public OperationStatusInfo<string> Registration(UserDTO userDto)
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                return base.Channel.Registration(userDto);
            }
        }

        public OperationStatusInfo<UserDTO> Login(UserLoginDTO userLoginDto)
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                return base.Channel.Login(userLoginDto);
            }
        }

        public OperationStatusInfo<List<MessageDTO>> GetAllMessages(UserDTO userDto)
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                return base.Channel.GetAllMessages(userDto);
            }
        }

        public OperationStatusInfo<List<UserDTO>> GetAllUsers()
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                return base.Channel.GetAllUsers();
            }
        }

        public OperationStatusInfo<string> SendMessage(MessageDTO messageDto)
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                return base.Channel.SendMessage(messageDto);
            }
        }
    }
}