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
    using Chat.DTO;
    using Chat.DTO.Response;

    public class UserService : BaseService<User, UserDTO>, IUserService
    {
        private readonly ILoggingService loggingService;

        private readonly IOperationStatusService operationStatusService;

        public UserService(
            IUnitOfWork unitOfWork,
            IRepository<User> currentRepository,
            ILoggingService loggingService,
            IOperationStatusService operationStatusService)
            : base(unitOfWork, currentRepository)
        {
            this.loggingService = loggingService;
            this.operationStatusService = operationStatusService;
        }

        public OperationStatusInfo<string> Registration(UserDTO userDto)
        {
            this.loggingService.Info($"Регистрация нового пользователя {userDto}.");

            // валидация
            var validator = new UserValidator();
            var validationResult = validator.Validate(userDto);

            // проверка валидации
            if (!validationResult.IsValid)
            {
                return this.operationStatusService.HandleValidationError(
                    $"Ошибка при валидации пользователя {userDto}. Текст ошибки: {validationResult}",
                    validationResult.ToString());
            }

            try
            {
                var user = DTOService.ToEntity<UserDTO, User>(userDto);

                // проверка существующего пользователя (одинаковый логин)
                if (this.currentRepository.Get(new List<Expression<Func<User, bool>>> { u => u.Login == user.Login }).Any())
                {
                    return this.operationStatusService.HandleErrorOperation($"Пользователь {user.Login} уже существует.", string.Empty);
                }

                user.CreatedOn = DateTime.Now;
                this.currentRepository.Insert(user);

                return this.operationStatusService.HandleSuccessOperation($"Пользователь {user.Login} успешно зарегистрирован.", string.Empty);
            }
            catch (Exception exception)
            {
                return this.operationStatusService.HandleException(exception, string.Empty);
            }
        }

        public OperationStatusInfo<UserDTO> Login(UserLoginDTO userLoginDto)
        {
            this.loggingService.Info($"Попытка входа пользователя {userLoginDto} в систему.");

            // валидация
            var validator = new UserLoginValidator();
            var validationResult = validator.Validate(userLoginDto);

            // проверка валидации
            if (!validationResult.IsValid)
            {
                return this.operationStatusService.HandleValidationError<UserDTO>(
                    $"Ошибка при валидации идентификационных данных: {userLoginDto}. Текст ошибки: {validationResult}",
                    null);
            }

            try
            {
                var loginUser = DTOService.ToEntity<UserLoginDTO, User>(userLoginDto);
                var user = this.currentRepository.Get(
                    new List<Expression<Func<User, bool>>>
                        {
                            u => u.Login == loginUser.Login && u.Password == loginUser.Password
                        }).FirstOrDefault();

                // проверка существует ли пользователь
                return user != null
                           ? this.operationStatusService.HandleSuccessOperation(
                               $"Пользователь {loginUser.Login} успешно идентифицирован.",
                               DTOService.ToDTO<User, UserDTO>(user))
                           : this.operationStatusService.HandleErrorOperation<UserDTO>(
                               $"Пользователя {loginUser.Login} не существует.", null);
            }
            catch (Exception exception)
            {
                return this.operationStatusService.HandleException<UserDTO>(exception, null);
            }
        }

        public OperationStatusInfo<List<UserDTO>> GetAllUsers()
        {
            this.loggingService.Info($"Попытка получения всех пользователей.");
            try
            {
                var users = this.currentRepository.Get();
                var userDtoList = users.Select(DTOService.ToDTO<User, UserDTO>).ToList();

                return this.operationStatusService.HandleSuccessOperation(
                    $"Список пользователей ({userDtoList.Count}).",
                    userDtoList);
            }
            catch (Exception exception)
            {
                return this.operationStatusService.HandleException<List<UserDTO>>(exception, null);
            }
        }
    }
}
