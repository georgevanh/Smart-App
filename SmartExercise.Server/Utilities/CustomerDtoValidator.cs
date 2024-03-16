using SmartExercise.Server.Models;

namespace SmartExercise.Server.Utilities
{
    using FluentValidation;
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid email address.");
            RuleFor(x => x.MobileNumber).NotEmpty().WithMessage("Mobile number is required.")
                .Matches(@"^(\+?61|0)4\d{8}$").WithMessage("Invalid Australian mobile number.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
        }
    }

}
