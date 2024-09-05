
using BusinessLayer.DTOs;
using FluentValidation;

namespace BusinessLayer.Validator
{
    public class UserRegisterValidator : AbstractValidator<UserRegister>
    {
        public UserRegisterValidator()
        {
            RuleFor(t => t.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address!")
                .MaximumLength(100);

            RuleFor(t => t.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters !");

            RuleFor(t => t.Username)
               .NotEmpty().WithMessage("Please Enter Your Username!");
        }
    }
}
