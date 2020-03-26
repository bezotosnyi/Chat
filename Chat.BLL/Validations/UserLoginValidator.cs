namespace Chat.BLL.Validations
{
    using Chat.DTO;

    using FluentValidation;

    public class UserLoginValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginValidator()
        {
            this.RuleFor(u => u.Login)
                .NotNull().WithMessage("Логин обязательное поле.")
                .Length(4, 20).WithMessage("Длина логина должна быть 4-20 символов.");

            this.RuleFor(u => u.Password)
                .NotNull().WithMessage("Пароль обязательное поле.")
                .Length(4, 20).WithMessage("Длина пароля должна быть 4-20 символов.");
        }
    }
}