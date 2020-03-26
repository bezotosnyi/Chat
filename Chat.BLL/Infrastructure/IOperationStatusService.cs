namespace Chat.BLL.Infrastructure
{
    using System;

    using Chat.DTO.Response;

    public interface IOperationStatusService
    {
        OperationStatusInfo<T> HandleSuccessOperation<T>(string message, T attachedObject);

        OperationStatusInfo<T> HandleErrorOperation<T>(string message, T attachedObject);

        OperationStatusInfo<T> HandleValidationError<T>(string message, T attachedObject);

        OperationStatusInfo<T> HandleException<T>(Exception exception, T attachedObject);
    }
}