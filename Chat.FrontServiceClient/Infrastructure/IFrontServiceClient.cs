namespace Chat.FrontServiceClient.Infrastructure
{
    using System.Collections.Generic;

    using Chat.DTO;
    using Chat.DTO.Response;

    public interface IFrontServiceClient
    {
        OperationStatusInfo<string> Registration(UserDTO userDto);

        OperationStatusInfo<UserDTO> Login(UserLoginDTO userLoginDto);

        OperationStatusInfo<List<MessageDTO>> GetAllMessages(UserDTO userDto);

        OperationStatusInfo<List<UserDTO>> GetAllUsers();

        OperationStatusInfo<string> SendMessage(MessageDTO messageDto);
    }
}