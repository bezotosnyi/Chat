namespace Chat.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Chat.BLL.Infrastructure;
    using Chat.BLL.Validations;
    using Chat.DAL.Infrastructure;
    using Chat.Domain.Entities;
    using Chat.Domain.Entities.Enum;
    using Chat.DTO;
    using Chat.DTO.Response;

    public class MessageService : BaseService<Message, MessageDTO>, IMessageService
    {
        private readonly ILoggingService loggingService;

        private readonly IOperationStatusService operationStatusService;

        public MessageService(
            IUnitOfWork unitOfWork,
            IRepository<Message> currentRepository,
            ILoggingService loggingService,
            IOperationStatusService operationStatusService)
            : base(unitOfWork, currentRepository)
        {
            this.loggingService = loggingService;
            this.operationStatusService = operationStatusService;
        }

        public OperationStatusInfo<List<MessageDTO>> GetAllMessages(UserDTO userDto)
        {
            this.loggingService.Info($"Попытка получения всех сообщений пользователю {userDto}.");

            try
            {
                var messages = this.currentRepository.Get(new List<Expression<Func<Message, bool>>>()
                                                              {
                                                                  m => m.MessageType == MessageType.Public
                                                                       || m.UserFrom.Id == userDto.Id
                                                                       || m.UserTo.Id == userDto.Id
                                                              });
                foreach (var message in messages)
                {
                    message.UserFrom = this.unitOfWork.UserRepository.GetById(message.UserFromId);
                    message.UserTo = this.unitOfWork.UserRepository.GetById(message.UserToId);
                }

                var messageDtoList = messages.Select(DTOService.ToDTO<Message, MessageDTO>).ToList();

                return this.operationStatusService.HandleSuccessOperation(
                    $"Список сообщений ({messageDtoList.Count}).",
                    messageDtoList);
            }
            catch (Exception exception)
            {
                return this.operationStatusService.HandleException<List<MessageDTO>>(exception, null);
            }
        }

        public OperationStatusInfo<string> SendMessage(MessageDTO messageDto)
        {
            this.loggingService.Info($"Попытка валидации сообщения {messageDto}.");

            // валидация
            var validator = new MessageValidator();
            var validationResult = validator.Validate(messageDto);

            // проверка валидации
            if (!validationResult.IsValid)
            {
                return this.operationStatusService.HandleValidationError(
                    $"Ошибка при валидации сообщения: {messageDto}. Текст ошибки: {validationResult}",
                    validationResult.ToString());
            }

            try
            {
                var message = DTOService.ToEntity<MessageDTO, Message>(messageDto);
                message.UserFrom = null;
                message.UserTo = null;
                this.currentRepository.Insert(message);
                return this.operationStatusService.HandleSuccessOperation(
                    $"Сообщение {messageDto} успешно отправлено.", string.Empty);
            }
            catch (Exception exception)
            {
                return this.operationStatusService.HandleException(exception, string.Empty);
            }
        }
    }
}
