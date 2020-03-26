namespace Chat.BLL.Infrastructure
{
    using System.Collections.Generic;

    using Chat.DTO;
    using Chat.DTO.Response;

    public interface IMessageService
    {
        OperationStatusInfo<List<MessageDTO>> GetAllMessages(UserDTO userDto);

        OperationStatusInfo<string> SendMessage(MessageDTO messageDto);
    }
}
