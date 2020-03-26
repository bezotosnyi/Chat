namespace Chat.Service.Front
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    using Chat.DTO;
    using Chat.DTO.Response;
    
    [ServiceContract]
    public interface IChatService
    {
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "/api/chat/users/registration",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        OperationStatusInfo<string> Registration(UserDTO userDto);

        [WebInvoke(
            Method = "POST",
            UriTemplate = "/api/chat/users/login",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        OperationStatusInfo<UserDTO> Login(UserLoginDTO userLoginDto);

        [WebInvoke(
            Method = "POST",
            UriTemplate = "/api/chat/messages/getMessages",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        OperationStatusInfo<List<MessageDTO>> GetAllMessages(UserDTO userDto);

        [WebInvoke(
            Method = "GET",
            UriTemplate = "/api/chat/users/getAllUsers",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        OperationStatusInfo<List<UserDTO>> GetAllUsers();

        [WebInvoke(
            Method = "PUT",
            UriTemplate = "/api/chat/messages/sendMessage",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        OperationStatusInfo<string> SendMessage(MessageDTO messageDto);
    }
}
