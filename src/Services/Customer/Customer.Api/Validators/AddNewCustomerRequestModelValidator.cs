using Customer.Api.Models.Customer;
using FluentValidation;

namespace Customer.Api.Validators
{
    public class AddNewCustomerRequestModelValidator : AbstractValidator<AddNewCustomerRequestModel>
    {
        public AddNewCustomerRequestModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email cannot be null or empty")
                .NotNull()
                .WithMessage("Email cannot be null or empty")
                .MaximumLength(255)
                .WithMessage("Email cannot be longer than 255 characters");
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password cannot be null or empty")
                .NotNull()
                .WithMessage("Password cannot be null or empty")
                .MaximumLength(255)
                .WithMessage("Password cannot be longer than 255 characters");
        }
    }
}