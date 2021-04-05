using FluentValidation;
using Queue.Api.Models;

namespace Queue.Api.Validators
{
    public class SendMessageRequestModelValidator : AbstractValidator<SendMessageRequestModel>
    {
        public SendMessageRequestModelValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty()
                .WithMessage("Message cannot be null or empty.")
                .NotNull()
                .WithMessage("Message cannot be null or empty.");

            RuleFor(x => x.QueueName)
                .NotEmpty()
                .WithMessage("QueueName cannot be null or empty.")
                .NotNull()
                .WithMessage("QueueName cannot be null or empty.");
        }
    }
}