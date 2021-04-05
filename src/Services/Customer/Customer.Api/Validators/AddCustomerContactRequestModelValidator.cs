using Customer.Api.Models.Customer;
using FluentValidation;

namespace Customer.Api.Validators
{
    public class AddCustomerContactRequestModelValidator : AbstractValidator<AddCustomerContactRequestModel>
    {
        public AddCustomerContactRequestModelValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address cannot be null or empty.")
                .NotNull()
                .WithMessage("Address cannot be null or empty.")
                .MaximumLength(255)
                .WithMessage("Address cannot be longer than 255 characters.");
            
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone cannot be null or empty.")
                .NotNull()
                .WithMessage("Phone cannot be null or empty.")
                .MaximumLength(11)
                .WithMessage("Phone cannot be longer than 11 characters.");
        }
    }
}