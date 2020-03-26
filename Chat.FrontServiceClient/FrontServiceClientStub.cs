namespace Chat.FrontServiceClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chat.DTO;
    using Chat.DTO.Response;
    using Chat.FrontServiceClient.Infrastructure;

    public class FrontServiceClientStub : IFrontServiceClient
    {
        private readonly UserLoginDTO userLogin = new UserLoginDTO("admin", "admin");

        private readonly List<MessageDTO> messages = new List<MessageDTO>
                                                         {
                                                             new MessageDTO
                                                                 {
                                                                     Id = 1,
                                                                     CreatedOn = DateTime.Now,
                                                                     MessageType = MessageTypeDTO.Public,
                                                                     UserFrom = new UserDTO
                                                                                {
                                                                                    FirstName = "Дима",
                                                                                    LastName = "Безотосный"
                                                                                },
                                                                     MessageContent = "Сообщение 1"
                                                                 },

                                                             new MessageDTO
                                                                 {
                                                                     Id = 2,
                                                                     CreatedOn = DateTime.Now,
                                                                     MessageType = MessageTypeDTO.Private,
                                                                     UserFrom = new UserDTO
                                                                                {
                                                                                    FirstName = "Настя",
                                                                                    LastName = "Клименко"
                                                                                },
                                                                     MessageContent = "Сообщение 2"
                                                                 }
                                                         };

        private readonly List<UserDTO> users = new List<UserDTO>
                                                   {
                                                       new UserDTO
                                                           {
                                                               Id = 1,
                                                               FirstName = "Дима",
                                                               LastName = "Безотосный"
                                                           },
                                                       new UserDTO
                                                           {
                                                               Id = 2,
                                                               FirstName = "Настя",
                                                               LastName = "Клименко"
                                                           }
                                                   };

        public OperationStatusInfo<string> Registration(UserDTO userDto)
        {
            if (userDto == this.userLogin)
                return new OperationStatusInfo<string>(OperationStatus.Success, "Успешная регистрация.");

            return new OperationStatusInfo<string>(OperationStatus.Fail, "Неуспешная регистрация.");
        }

        public OperationStatusInfo<UserDTO> Login(UserLoginDTO userLoginDto)
        {
            if (userLoginDto == this.userLogin)
                return new OperationStatusInfo<UserDTO>(OperationStatus.Success, "Успешный вход.", new UserDTO { Id = 3, LastName = "test client" });

            return new OperationStatusInfo<UserDTO>(OperationStatus.Fail, "Неуспешный вход.");
        }

        public OperationStatusInfo<List<MessageDTO>> GetAllMessages(UserDTO userDto)
        {
            return new OperationStatusInfo<List<MessageDTO>>(OperationStatus.Success, "Success", this.messages);
        }

        public OperationStatusInfo<List<UserDTO>> GetAllUsers()
        {
            return new OperationStatusInfo<List<UserDTO>>(OperationStatus.Success, "Success", this.users);
        }

        public OperationStatusInfo<string> SendMessage(MessageDTO messageDto)
        {
            messageDto.Id = this.messages.LastOrDefault()?.Id + 1 ?? 1;
            this.messages.Add(messageDto);
            return new OperationStatusInfo<string>(OperationStatus.Success, "Ok");
        }
    }
}