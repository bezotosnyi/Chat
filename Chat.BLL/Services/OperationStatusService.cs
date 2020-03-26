namespace Chat.BLL.Services
{
    using System;

    using Chat.BLL.Infrastructure;
    using Chat.DTO.Response;

    public class OperationStatusService : IOperationStatusService
    {
        private readonly ILoggingService loggingService;

        public OperationStatusService(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        public OperationStatusInfo<T> HandleSuccessOperation<T>(string message, T attachedObject)
        {
            this.loggingService.Info(message);
            return new OperationStatusInfo<T>(OperationStatus.Success, message, attachedObject);
        }

        public OperationStatusInfo<T> HandleErrorOperation<T>(string message, T attachedObject)
        {
            this.loggingService.Warn(message);
            return new OperationStatusInfo<T>(OperationStatus.Fail, message, attachedObject);
        }

        public OperationStatusInfo<T> HandleValidationError<T>(string message, T attachedObject)
        {
            this.loggingService.Warn(message);
            return new OperationStatusInfo<T>(OperationStatus.Fail, message, attachedObject);
        }

        public OperationStatusInfo<T> HandleException<T>(Exception exception, T attachedObject)
        {
            // стек трейс не нужен пользователю
            var message = $"Возникла следующая исключительная ситуация: '{exception.Message}'.";

            // пишем его только в лог
            this.loggingService.Error($"{message} Стек вызова: '{exception.StackTrace}'.");

            return new OperationStatusInfo<T>(OperationStatus.Fail, message, attachedObject);
        }
    }
}