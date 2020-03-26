namespace Chat.BLL.Validations
{
    using Chat.DTO;

    using FluentValidation;

    public class MessageValidator : AbstractValidator<MessageDTO>
    {
        public MessageValidator()
        {
            this.RuleFor(m => m.MessageContent).NotNull().NotEmpty().WithMessage("Невозможно отправить пустое сообщение.");
        }
    }
}
