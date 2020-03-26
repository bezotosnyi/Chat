namespace Chat.FrontServiceClient
{
    using System.Collections.Generic;

    using Chat.DTO;
    using Chat.DTO.Response;
    using Chat.FrontServiceClient.Infrastructure;
    using Chat.RestClient.Infrastructure;

    public class FrontServiceClient : IFrontServiceClient
    {
        private readonly IChatService chatServiceClient;

        /*public FrontServiceClient()
        {
            this.chatServiceClient = new ChatServiceClient("http://localhost:8080");
        }*/

        public FrontServiceClient(IChatService chatServiceClient)
        {
            this.chatServiceClient = chatServiceClient;
        }

        public OperationStatusInfo<string> Registration(UserDTO userDto)
        {
            return this.chatServiceClient.Registration(userDto);
        }

        public OperationStatusInfo<UserDTO> Login(UserLoginDTO userLoginDto)
        {
            return this.chatServiceClient.Login(userLoginDto);
        }

        public OperationStatusInfo<List<MessageDTO>> GetAllMessages(UserDTO userDto)
        {
            return this.chatServiceClient.GetAllMessages(userDto);
        }

        public OperationStatusInfo<List<UserDTO>> GetAllUsers()
        {
            return this.chatServiceClient.GetAllUsers();
        }

        public OperationStatusInfo<string> SendMessage(MessageDTO messageDto)
        {
            return this.chatServiceClient.SendMessage(messageDto);
        }
    }
}