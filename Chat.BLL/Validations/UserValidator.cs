namespace Chat.BLL.Validations
{
    using System;

    using Chat.DTO;

    using FluentValidation;

    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            this.RuleFor(u => u.FirstName).NotNull().NotEmpty().WithMessage("Фамилия пользователя обязательное поле.");

            this.RuleFor(u => u.LastName).NotNull().NotEmpty().WithMessage("Имя пользователя обязательное поле.");

            this.RuleFor(u => u.DateOfBirthday).Must(ValidDate).WithMessage("Дата не может быть пустой и должна быть больше текущей.");

            this.RuleFor(u => u.Email)
                .NotNull().NotEmpty().WithMessage("Електронная почта обязательное поле.")
                .EmailAddress().WithMessage("Требуется правильная електронная почта.");

            this.RuleFor(u => new UserLoginDTO { Login = u.Login, Password = u.Password })
                .SetValidator(new UserLoginValidator());
        }

        private static bool ValidDate(DateTime? date)
        {
            return date.HasValue && !date.Value.Equals(default(DateTime)) && date.Value < DateTime.Now.Subtract(new TimeSpan(0, 1, 0));
        }
    }
}
